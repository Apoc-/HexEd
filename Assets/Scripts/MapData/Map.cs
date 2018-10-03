using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using Assets.Scripts.Systems.MapSystem;
using MapData;
using UnityEngine;

public struct MapExtents
{
    public float Width { get; set; }
    public float Height { get; set; }
    public float UpperBound { get; set; }
    public float LeftBound { get; set; }
}

public enum NeighbourIndex
{
    /*
     *  ab
     * f  c
     *  ed
     */
    NeighbourA,
    NeighbourB,
    NeighbourC,
    NeighbourD,
    NeighbourE,
    NeighbourF,
}


public class Map : MonoBehaviour
{
    private float tileSpacing = 0f;
    private float outerRadius = 0.5f;
    private float innerRadius = 0.5f * Mathf.Sqrt(3) / 2;
    private float baseHeight = -4f;

    public MapExtents Extents { get; set; } // TODO remove

    public Dictionary<Vector2, Tile> Tiles { get; set; } = new Dictionary<Vector2, Tile>();

    public void SetTileHeight(Vector2 position, float newHeight)
    {
        if (!Tiles.ContainsKey(position))
        {
            return; // TODO: error
        }

        Tiles[position].Height = newHeight;
        var pos = Tiles[position].transform.position;
        pos.y = newHeight;

        Tiles[position].transform.position = pos;
    }

    public void SetTileType(Vector2 position, TileType type)
    {
        if (!Tiles.ContainsKey(position))
        {
            Tiles[position] = TileProvider.GetTile(type);
            Tiles[position].transform.parent = transform;
            Tiles[position].transform.position = GetPositionForCoordinate(position);
            Tiles[position].Position = position;
            Tiles[position].Height = baseHeight;
        }
        else
        {
            Tiles[position].Type = type;
            TileProvider.SetTileMaterial(Tiles[position]);
        }


        // TODO: Refactor
        // Check the fields around and create VOID tiles, if needed.
        if (type != TileType.Void)
        {
            /*
            // check left
            Vector2 pos = position + new Vector2(-1, 0);
            if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);

            // check right
            pos = position + new Vector2(1, 0);
            if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);

            pos = position + new Vector2(0, -1);
            if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);
            pos = position + new Vector2(0, 1);
            if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);

            if (position.y % 2 == 0)
            {
                pos = position + new Vector2(-1, -1);
                if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);
                pos = position + new Vector2(-1, 1);
                if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);
            }
            else
            {
                pos = position + new Vector2(1, -1);
                if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);
                pos = position + new Vector2(1, 1);
                if (!Tiles.ContainsKey(pos)) SetTileType(pos, TileType.Void);
            }
            //*/


            foreach (NeighbourIndex idx in Enum.GetValues(typeof(NeighbourIndex)))
            {
                var Ypos = GetNeighbourPosition(position, idx);
                if (!Tiles.ContainsKey(Ypos)) SetTileType(Ypos, TileType.Void);
            }
        }

        CleanupTilesAroundPosition(position);
    }

    private Vector2 GetNeighbourPosition(Vector2 position, NeighbourIndex neighbour)
    {
        int evenY = Math.Abs((int) position.y) % 2;


        switch (neighbour)
        {
            case NeighbourIndex.NeighbourA:
                return position + new Vector2(-1 + evenY, -1);
            case NeighbourIndex.NeighbourB:
                return position + new Vector2(0 + evenY, -1);
            case NeighbourIndex.NeighbourC:
                return position + new Vector2(1, 0);
            case NeighbourIndex.NeighbourD:
                return position + new Vector2(0 + evenY, 1);
            case NeighbourIndex.NeighbourE:
                return position + new Vector2(-1 + evenY, 1);
            case NeighbourIndex.NeighbourF:
                return position + new Vector2(-1, 0);
            default:
                throw new ArgumentOutOfRangeException(nameof(neighbour), neighbour, null);
        }
    }


    private void CleanupTilesAroundPosition(Vector2 position)
    {
        foreach (NeighbourIndex idx in Enum.GetValues(typeof(NeighbourIndex)))
        {
            var newPos = GetNeighbourPosition(position, idx);
            if (!Tiles.ContainsKey(newPos))
                continue;
            if (Tiles[newPos].Type != TileType.Void)
                continue;
            // count the tiles around that are NOT void or null
            var count = 0;
            foreach (NeighbourIndex subIdx in Enum.GetValues(typeof(NeighbourIndex)))
            {
                var subPos = GetNeighbourPosition(newPos, subIdx);
                if (Tiles.ContainsKey(subPos) && Tiles[subPos].Type != TileType.Void)
                    count++;
            }

            if (count == 0)
            {
                Destroy(Tiles[newPos].GetComponent<MeshRenderer>());
                Destroy(Tiles[newPos].GetComponent<MeshFilter>());
                Destroy(Tiles[newPos].GetComponent<MeshCollider>());
                Destroy(Tiles[newPos].GetComponent<Tile>());
                Tiles[newPos].transform.name += " deleted";
                Destroy(Tiles[newPos].gameObject);
                Tiles.Remove(newPos);
                CleanupTilesAroundPosition(newPos);
            }
        }
    }

    public Vector3 GetPositionForCoordinate(Vector2 position)
    {
        var positionForCoordinate = new Vector3
        {
            x = (innerRadius * 2 + tileSpacing) * position.x,
            z = -(outerRadius * 2 + tileSpacing) * (3f / 4f) * position.y,
            y = baseHeight
        };
        if (Math.Floor(position.y) % 2 == 0)
        {
            positionForCoordinate.x -= (innerRadius * 2 + tileSpacing) / 2;
        }

        return positionForCoordinate;
    }
}