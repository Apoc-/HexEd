using System;
using System.Collections.Generic;
using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using MapData;
using UnityEngine;

namespace HexEd.Tools
{
    public struct State
    {
        public TileType Type;
        public float Height;
    }

    public struct Action
    {
        public Action(Tile tile) : this()
        {
            Tile = tile;
            OldState = new State
            {
                Type = tile.Type,
                Height = tile.Height
            };
            NewState = new State
            {
                Type = tile.Type,
                Height = tile.Height
            };
        }

        public void Execute()
        {
            SetState(OldState, NewState);
        }

        public void Revert()
        {
            SetState(NewState, OldState);
        }

        private void SetState(State old, State newState)
        {
            if (old.Type != newState.Type)
            {
                MapManager.Instance.Map.SetTileType(Tile.Position, newState.Type);
            }

            if (Math.Abs(old.Height - newState.Height) > 0.001)
            {
                MapManager.Instance.Map.SetTileHeight(Tile.Position, newState.Height);
            }
        }

        public Tile Tile;
        public State OldState;
        public State NewState;
    }

    public struct ActionGroup
    {
        public List<Action> Actions;
    }
}