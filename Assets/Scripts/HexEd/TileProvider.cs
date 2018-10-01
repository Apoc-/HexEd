using System;
using System.Collections.Generic;
using Assets.Scripts.MapData;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Systems.MapSystem
{
    class TileProvider : MonoBehaviour
    {
        private static string tileMaterialsPath = "Materials/Tiles/";
        private static string tileBottomMaterialName = "Bottom";
        private static string tilePrefabPath = "Prefabs/Tile";

        private static readonly Dictionary<TileType, List<String>> TileMaterials = new Dictionary<TileType, List<string>>
        {
            { TileType.Path, new List<string>{"Path"}},
            { TileType.Buildslot, new List<string>{"Buildslot"}},
            { TileType.Start, new List<string>{"StartEnd"}},
            { TileType.End, new List<string>{"StartEnd"}},
            { TileType.MountainTop, new List<string>{"MountainTop"}},
            { TileType.Mountain, new List<string>{"Mountain"}},
            { TileType.Sand, new List<string>{"Sand"}},
            { TileType.Lava, new List<string>{"Lava"}},
            { TileType.VolcanoLava, new List<string>{"Lava"}},
            { TileType.Void, new List<string>{"Void"}}
        };

        private static readonly Random Rng = new Random();

        public static Tile GetTile(TileType type, int variant = 0)
        {
            Tile tile = Instantiate(Resources.Load<Tile>(tilePrefabPath));

            tile.Type = type;
            tile.Variant = variant;

            SetTileMaterial(tile);

            return tile;
        }

        public static void SetTileMaterial(Tile tile)
        {
            var materials = TileMaterials[tile.Type];
            var materialName = materials[tile.Variant];
            var topMaterial = Resources.Load<Material>(tileMaterialsPath + materialName);
            var bottomMaterial = Resources.Load<Material>(tileMaterialsPath + tileBottomMaterialName);
            
            var renderer = tile.GetComponent<MeshRenderer>();
            var mats = renderer.materials;

            mats[0] = topMaterial;

            if (tile.Type == TileType.Void)
            {
                mats[1] = topMaterial;
            }
            else
            {
                mats[1] = bottomMaterial;
            }

            renderer.materials = mats;
        }
    }
}
