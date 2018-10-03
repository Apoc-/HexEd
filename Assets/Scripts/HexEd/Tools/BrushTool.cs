using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using MapData;
using UnityEngine;

namespace HexEd.Tools
{
    public class BrushTool : Tool
    {
        public  TileType selectedType = TileType.Buildslot;

        public override void OnTileClick(Tile tile)
        {
            MapManager.Instance.Map.SetTileType(tile.Position, selectedType);
        }

        public override void OnTileDrag(Tile tile)
        {
            OnTileClick(tile);
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