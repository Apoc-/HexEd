using Assets.Scripts.MapData;
using UnityEngine;

namespace MapData
{
    public class Tile : MonoBehaviour
    {
        public TileType Type { get; set; }
        public Vector2 Position { get; set; }
        public float Height { get; set; }

        public int Variant { get; set; }
    }
}