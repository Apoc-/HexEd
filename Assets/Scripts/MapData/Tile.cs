using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MapData
{
    public class Tile : MonoBehaviour
    {
        public TileType Type { get; set; }
        public Vector2 Position { get; set; }
        public float Height { get; }

        public int Variant { get; set; }
    }
}
