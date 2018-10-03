using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MapData;
using Assets.Scripts.Systems.MapSystem;
using HexEd;
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


        // TODO: Refactor into separate class

        private List<List<Action>> _actionHistory = new List<List<Action>>();
        private List<List<Action>> _actionFuture = new List<List<Action>>();
        private List<Action> _actionGroup;

        public void NewActionGroup()
        {
            if (_actionGroup != null && _actionGroup.Count > 0)
            {
                _actionHistory.Add(new List<Action>(_actionGroup));
                _actionGroup = null;
            }

            _actionGroup = new List<Action>();
        }

        public void AddAction(Action action)
        {
            // Adding ANY action removes the whole action future!
            _actionFuture = new List<List<Action>>();

            _actionGroup.Add(action);
            action.Execute();
        }

        public void RevertLastStep()
        {
            NewActionGroup();

            if (_actionHistory.Count == 0)
                return;
            
            // Get the last ActionGroup, revert it and add it to the action future.
            var findLast = _actionHistory.Last();
            if (findLast == null)
                return;

            for (var i = findLast.Count - 1; i >= 0; i--)
            {
                findLast[i].Revert();
            }

            _actionFuture.Add(findLast);
            _actionHistory.Remove(findLast);
        }

        public void RedoNextStep()
        {
            NewActionGroup();

            if (_actionFuture.Count == 0)
                return;

            var actions = _actionFuture.Last();
            if (actions == null)
                return;

            foreach (var action in actions)
            {
                action.Execute();
            }

            _actionHistory.Add(actions);
            _actionFuture.Remove(actions);
        }
    }
}