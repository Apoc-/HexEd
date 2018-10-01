using Assets.Scripts.MapData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.HexEd
{
    public class TileSelectionManager : MonoBehaviour
    {
        private Tile currentSelectedTile;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Tile tile = CheckForTile();

                if (tile != null)
                {
                    SelectTile(tile);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && currentSelectedTile != null)
            {
                DeselectTile();
            }
        }

        private Tile CheckForTile()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            LayerMask mask = LayerMask.GetMask("Tiles");

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) return null;

            return hit.transform.gameObject.GetComponent<Tile>();
        }


        public void SelectTile(Tile tile)
        {
            currentSelectedTile = tile;
            var mesh = currentSelectedTile.GetComponent<MeshRenderer>();
            mesh.material.color = Color.green;
        }

        public void DeselectTile()
        {
            var mesh = currentSelectedTile.GetComponent<MeshRenderer>();
            mesh.material.color = Color.white;
            currentSelectedTile = null;
        }
    }
}