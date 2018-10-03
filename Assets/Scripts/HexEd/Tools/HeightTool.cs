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
            initialHeight = tile.Height;
            initialMousePos = firstMousePos;
            initialTilePosition = tile.gameObject.transform.position;
        }

        public override void OnTileScroll(Tile tile, Vector3 currentMousePos)
        {
            int zDiff = (int) ((currentMousePos - initialMousePos).y / 15);
            tile.gameObject.transform.position = initialTilePosition + new Vector3(0, zDiff * 0.1f, 0);
            tile.Height = initialHeight + zDiff * 0.1f;
        }

        public override void OnTileScrollStop(Tile tile, Vector3 currentMousePos)
        {
        }
    }
}