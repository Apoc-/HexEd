using System.Collections.Generic;
using Assets.Scripts.MapData;
using Assets.Scripts.Systems.MapSystem;
using HexEd;
using MapData;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.HexEd
{
    public class MapManager : Singleton<MapManager>
    {
        public Map Map { get; private set; }

        private float tileSpacing = 0f;
        private float outerRadius = 0.5f;
        private float innerRadius = 0.5f * Mathf.Sqrt(3) / 2;
        private float baseHeight = -4f;

        void Start()
        {
            GenerateDefaultMap();
            // GenerateEmptyMap(16, 12);
        }

        private void GenerateDefaultMap()
        {
            Map = new GameObject("Map", typeof(Map)).GetComponent<Map>();
            Map.Tiles = new Dictionary<Vector2, Tile>();
            Map.SetTileType(Vector2.zero, TileType.Buildslot);
            Map.SetTileType(new Vector2(3, 3), TileType.Buildslot);
            Map.SetTileType(new Vector2(1, 3), TileType.Buildslot);
            
            Map.SetTileType(new Vector2(-3, 7), TileType.Buildslot);
            Map.SetTileType(new Vector2(-1, 5), TileType.Buildslot);
            Map.SetTileType(new Vector2(9, 9), TileType.Buildslot);
            Map.SetTileType(new Vector2(9, -3), TileType.Buildslot);
            //Map.SetTileType(new Vector2(0, 1), TileType.Buildslot);
            //Map.SetTileType(new Vector2(0, 2), TileType.Buildslot);
            //Map.SetTileType(new Vector2(1, 0), TileType.Buildslot);
            //Map.SetTileType(new Vector2(2, 0), TileType.Buildslot);
            UiManager.Instance.Camera.CenterCameraToMap(Map);
        }

/*
        public void GenerateEmptyMap(int width, int height)
        {
            Map = new GameObject("Map", typeof(Map)).GetComponent<Map>();
            Map.Tiles = new List<List<Tile>>();

            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                List<Tile> row = new List<Tile>();
                Map.Tiles.Add(row);

                for (int columnIndex = 0; columnIndex < width; columnIndex++)
                {
                    Tile tile = TileProvider.GetTile(TileType.Buildslot);
                    tile.transform.parent = Map.transform;
                    tile.Position = new Vector2(rowIndex, columnIndex);

                    Vector3 newPosition = new Vector3
                    {
                        x = (innerRadius * 2 + tileSpacing) * columnIndex,
                        z = -(outerRadius * 2 + tileSpacing) * (3f / 4f) * rowIndex,
                        y = baseHeight
                    };

                    if (rowIndex % 2 == 0)
                    {
                        newPosition.x -= (innerRadius * 2 + tileSpacing) / 2;
                    }

                    tile.transform.SetPositionAndRotation(newPosition, tile.transform.rotation);

                    row.Add(tile);
                }

            }

            CalculateMapExtents();
            UiManager.Instance.Camera.CenterCameraToMap(Map);
        }
*/
        /*
        private void CalculateMapExtents()
        {
            MapExtents extents = new MapExtents();

            extents.Width = Map.Tiles[0].Count * innerRadius * 2;

            if (Map.Tiles[0].Count % 2 != 0)
            {
                extents.Width += innerRadius;
            }

            var h = Map.Tiles.Count;
            extents.Height = Mathf.Ceil(h / 2f) * 2 * outerRadius + Mathf.Floor(h / 2f) * outerRadius;

            extents.LeftBound = Map.Tiles[0][0].transform.position.x - innerRadius;
            extents.UpperBound = Map.Tiles[0][0].transform.position.z + outerRadius;

            Map.Extents = extents;
        }

        public void CleanMap()
        {
            Map.Tiles.ForEach(row => row.ForEach(tile => Destroy(tile.gameObject)));
        }*/
    }
}