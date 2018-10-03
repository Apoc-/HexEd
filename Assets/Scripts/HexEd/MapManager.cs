﻿using System.Collections.Generic;
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
            if (Map != null)
            {
                Destroy(Map);
            }
            
            Map = new GameObject("Map", typeof(Map)).GetComponent<Map>();
            Map.Tiles = new Dictionary<Vector2, Tile>();
            Map.SetTileType(Vector2.zero, TileType.Buildslot);
            Map.SetTileType(new Vector2(3, 3), TileType.Buildslot);
            Map.SetTileType(new Vector2(1, 3), TileType.Buildslot);

            Map.SetTileType(new Vector2(-3, 7), TileType.Buildslot);
            Map.SetTileType(new Vector2(-1, 5), TileType.Buildslot);
            Map.SetTileType(new Vector2(9, 9), TileType.Buildslot);
            Map.SetTileType(new Vector2(9, -3), TileType.Buildslot);
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