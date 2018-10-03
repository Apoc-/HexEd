using System;
using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using MapData;
using UnityEngine;

namespace HexEd.Tools
{
    public class HeightTool : Tool
    {
        private TileType selectedType = TileType.Buildslot;

        private float initialHeight = 0;
        private Vector3 initialTilePosition;
        private Vector3 initialMousePos;

        public override void OnTileClick(Tile tile)
        {
        }

        public override void OnTileDrag(Tile tile)
        {
        }

        public override void OnTileScrollStart(Tile tile, Vector3 firstMousePos)
        {
            MapManager.Instance.NewActionGroup();
            initialHeight = tile.Height;
            initialMousePos = firstMousePos;
            initialTilePosition = tile.gameObject.transform.position;
        }

        public override void OnTileScroll(Tile tile, Vector3 currentMousePos)
        {
            if (tile.Type == TileType.Void) return;
            
            int zDiff = (int) ((currentMousePos - initialMousePos).y / 15);
            var newHeight = Math.Max(-8, initialHeight + zDiff * 0.1f) ;
            newHeight = Math.Min(0, newHeight);
            
            var action = new Action(tile) {NewState = {Height = newHeight}};
            MapManager.Instance.AddAction(action);   
        }

        public override void OnTileScrollStop(Tile tile, Vector3 currentMousePos)
        {
        }
    }
}