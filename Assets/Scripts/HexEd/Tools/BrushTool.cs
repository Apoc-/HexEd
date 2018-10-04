using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using HexEd.Actions;
using MapData;
using UnityEngine;

namespace HexEd.Tools
{
    public class BrushTool : Tool
    {
        public TileType SelectedType { get; set; } = TileType.Buildslot;

        public override void OnTileClick(Tile tile)
        {
            ActionManager.Instance.StartNewActionGroup();
            OnTileDrag(tile);
        }

        public override void OnTileDrag(Tile tile)
        {
            if (tile.Type == SelectedType) return;
            var action = new TileAction(tile).SetNewType(SelectedType);
            ActionManager.Instance.AddAndExecuteAction(action);
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