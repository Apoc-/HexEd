using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MapData;
using Assets.Scripts.Systems.MapSystem;
using HexEd;
using HexEd.Actions;
using HexEd.Tools;
using MapData;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.HexEd
{
    public class MapManager : Singleton<MapManager>
    {
        public Map Map { get; private set; }
        public const float BaseHeight = -4f;

        private void Start()
        {
            GenerateDefaultMap();
            // GenerateEmptyMap(16, 12);
        }

        private void GenerateDefaultMap()
        {
            if (Map != null)
            {
                Destroy(Map);
            }

            Map = new GameObject("Map", typeof(Map)).GetComponent<Map>();
            Map.Tiles = new Dictionary<Vector2, Tile>();

            for (int x = -5; x < 5; x++)
            for (int y = -5; y < 5; y++)
                Map.SetTileType(new Vector2(x, y), TileType.Buildslot);

            UiManager.Instance.Camera.CenterCameraToMap(Map);
        }


        // TODO: Refactor into separate class

        public void RevertLastStep() => ActionManager.Instance.RevertLastStep();
        public void RedoNextStep() => ActionManager.Instance.RedoNextStep();

        // TODO: Refactor to separate class
        public void SaveMap()
        {
        }
    }
}