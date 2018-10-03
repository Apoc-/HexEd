using Assets.Scripts.MapData;
using MapData;
using UnityEngine;

namespace HexEd
{
    public struct State
    {
        public Vector2 Position;
        public TileType Type;
        public int Height;
    }

    public struct Action
    {
        public Tile Tile;
        public State OldState;
        public State NewState;
    }
}