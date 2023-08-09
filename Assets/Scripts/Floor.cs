using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<FloorColorMapping> floorColorMappings;

    private bool isPassed;

    private void Paint()
    {
        spriteRenderer.sprite = floorColorMappings
            .First(x => x.color.Equals(GameManager.Instance.activeLevel.paintColor)).sprite;

        isPassed = !isPassed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPassed) return;

        GameManager.Instance.IncreaseScore();
        
        if (collision.transform.CompareTag("Ball"))
            Paint();
    }
}

[Serializable]
public class FloorColorMapping
{
    public Color color;
    public Sprite sprite;
}