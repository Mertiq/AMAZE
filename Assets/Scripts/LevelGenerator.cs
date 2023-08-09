using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform wallsParent;
    [SerializeField] private Transform floorsParent;
    [SerializeField] private Transform ballParent;
    [SerializeField] private List<LevelColorMapping> levelColorMappings;

    [HideInInspector] public int blankTileCount;
    private Level level;
    private List<GameObject> instantiatedLevelParts = new();

    private void Start() => GenerateLevel();

    public void GenerateLevel()
    {        
        DestroyLevel();
        
        level = GameManager.Instance.activeLevel;
        blankTileCount = 0;

        for (var x = 0; x < level.map.width; x++)
        {
            for (var y = 0; y < level.map.height; y++)
                GenerateTile(x, y);
        }

        GameManager.Instance.targetScore = blankTileCount;
    }

    private void GenerateTile(int x, int y)
    {
        var color = level.map.GetPixel(x, y);

        var tile = levelColorMappings.First(levelColorMapping => levelColorMapping.color.Equals(color))
            .prefab;

        Transform parent = null;

        if (color.Equals(Color.blue))
        {
            parent = ballParent;

            var floor = levelColorMappings
                .First(levelColorMapping => levelColorMapping.color.Equals(Color.black)).prefab;

            blankTileCount++;

            var floorPart = Instantiate(floor, new Vector3(x, 0, y), floor.transform.rotation, floorsParent);
            instantiatedLevelParts.Add(floorPart);
        }
        else if (color.Equals(Color.black))
        {
            parent = floorsParent;
            blankTileCount++;
        }
        else if (color.Equals(Color.white))
        {
            parent = wallsParent;
        }

        var levelPart = Instantiate(tile, new Vector3(x, 0, y), tile.transform.rotation, parent);
        instantiatedLevelParts.Add(levelPart);
    }

    private void DestroyLevel()
    {
        for (var i = 0; i < instantiatedLevelParts.Count; i++)
            Destroy(instantiatedLevelParts[i]);
        instantiatedLevelParts.Clear();
    }
}

[Serializable]
public class LevelColorMapping
{
    public Color color;
    public GameObject prefab;
}