using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using UnityEngine;

public struct MapExtents
{
    public float Width { get; set; }
    public float Height { get; set; }
    public float UpperBound { get; set; }
    public float LeftBound { get; set; }
}

public class Map : MonoBehaviour
{
    private float width;
    private float height;

    public MapExtents Extents { get; set; }

    public List<List<Tile>> Tiles { get; set; } = new List<List<Tile>>();
}
