using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using HexEd.Actions;
using MapData;
using UnityEngine;

namespace HexEd.Tools
{
    public class EraserTool : Tool
    {
        public override void OnTileClick(Tile tile)
        {
            if (tile.Type == TileType.Void) return;
            ActionManager.Instance.StartNewActionGroup();
            OnTileDrag(tile);
        }

        public override void OnTileDrag(Tile tile)
        {
            if (tile.Type == TileType.Void) return;
            var action = new TileAction(tile)
                .SetNewType(TileType.Void)
                .SetNewHeight(MapManager.BaseHeight);
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