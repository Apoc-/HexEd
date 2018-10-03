using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using MapData;
using UnityEngine;

namespace HexEd.Tools
{
    public class BrushTool : Tool
    {
        public TileType selectedType = TileType.Buildslot;

        public override void OnTileClick(Tile tile)
        {
            MapManager.Instance.NewActionGroup();
            OnTileDrag(tile);
        }

        public override void OnTileDrag(Tile tile)
        {
            if (tile.Type == selectedType) return;
            var action = new Action(tile) {NewState = {Type = selectedType}};
            MapManager.Instance.AddAction(action);
        }

        public override void OnTileScrollStart(Tile tile, Vector3 firstMousePos)
        {
        }

        public override void OnTileScroll(Tile tile, Vector3 currentMousePos)
        {
        }

        public override void OnTileScrollStop(Tile tile, Vector3 currentMousePos)
        {
        }
    }
}