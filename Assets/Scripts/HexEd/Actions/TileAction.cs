using System;
using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using MapData;

namespace HexEd.Actions
{
    public class TileAction : Action
    {
        private struct TileActionState
        {
            public TileType Type;
            public float Height;
        }

        private readonly Tile _tile;
        private readonly TileActionState _oldState;
        private TileActionState _newState;

        public TileAction(Tile tile)
        {
            _tile = tile;
            _oldState = new TileActionState
            {
                Type = tile.Type,
                Height = tile.Height
            };
            _newState = new TileActionState
            {
                Type = tile.Type,
                Height = tile.Height
            };
        }

        public TileAction SetNewType(TileType type)
        {
            _newState.Type = type;
            return this;
        }

        public TileAction SetNewHeight(float newHeight)
        {
            _newState.Height = newHeight;
            return this;
        }


        public override void Execute()
        {
            SetState(_oldState, _newState);
        }

        public override void Revert()
        {
            SetState(_newState, _oldState);
        }

        private void SetState(TileActionState old, TileActionState newState)
        {
            if (old.Type != newState.Type)
            {
                MapManager.Instance.Map.SetTileType(_tile.Position, newState.Type);
            }

            if (Math.Abs(old.Height - newState.Height) > 0.001)
            {
                MapManager.Instance.Map.SetTileHeight(_tile.Position, newState.Height);
            }
        }
    }
}