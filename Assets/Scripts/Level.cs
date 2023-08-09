using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 0)]
public class Level : ScriptableObject
{
    public Texture2D map;
    public Color paintColor;
}