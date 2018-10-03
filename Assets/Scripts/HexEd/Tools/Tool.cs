
using UnityEngine;

namespace HexEd.Tools
{
    public abstract class Tool
    {
        public abstract void OnTileClick(MapData.Tile tile);
        public abstract void OnTileDrag(MapData.Tile tile);
        public abstract void OnTileScrollStart(MapData.Tile tile, Vector3 firstMousePos);
        public abstract void OnTileScroll(MapData.Tile tile, Vector3 currentMousePos);
        public abstract void OnTileScrollStop(MapData.Tile tile, Vector3 currentMousePos);
    }
}