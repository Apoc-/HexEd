using System;
using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using HexEd.Actions;
using MapData;
using UnityEngine;
using Action = System.Action;

namespace HexEd.Tools
{
    public class HeightTool : Tool
    {
        private float _initialHeight;
        private Vector3 _initialTilePosition;
        private Vector3 _initialMousePos;

        public override void OnTileClick(Tile tile)
        {
        }

        public override void OnTileDrag(Tile tile)
        {
        }

        public override void OnTileScrollStart(Tile tile, Vector3 firstMousePos)
        {
            ActionManager.Instance.StartNewActionGroup();
            _initialHeight = tile.Height;
            _initialMousePos = firstMousePos;
            _initialTilePosition = tile.gameObject.transform.position;
        }

        public override void OnTileScroll(Tile tile, Vector3 currentMousePos)
        {
            if (tile.Type == TileType.Void) return;

            var zDiff = (int) ((currentMousePos - _initialMousePos).y / 15);
            var newHeight = Math.Min(0, Math.Max(-8, _initialHeight + zDiff * 0.1f));

            var action = new TileAction(tile).SetNewHeight(newHeight);
            ActionManager.Instance.AddAndExecuteAction(action);
        }

        public override void OnTileScrollStop(Tile tile, Vector3 currentMousePos)
        {
        }
    }
}