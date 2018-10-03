using System;
using System.Collections.Generic;
using Assets.Scripts.HexEd;
using Assets.Scripts.MapData;
using MapData;
using UnityEngine;

namespace HexEd.Actions
{
    public abstract class Action
    {
        public abstract void Execute();
        public abstract void Revert();
    }
}