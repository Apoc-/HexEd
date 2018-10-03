using Assets.Scripts.MapData;
using HexEd.Tools;
using MapData;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Tool = HexEd.Tools.Tool;

namespace Assets.Scripts.HexEd
{
    public class TileSelectionManager : MonoBehaviour
    {
        [SerializeField] public TMP_Dropdown BrushDropdown;

        private readonly BrushTool _toolBrush = new BrushTool();
        private readonly EraserTool _toolEraser = new EraserTool();
        private readonly HeightTool _toolHeight = new HeightTool();


        private Tile _firstSelectedTile;
        private Tile _currentSelectedTile;
        private Tool _currentSelectedTool;
        private Tile _lastDraggedTile;

        private Vector3 _initialMousePosition;
        private Vector3 _lastMousePosition;

        private bool _mouseLdown = false;

        public void Start()
        {
            _currentSelectedTool = _toolBrush;
        }

        public void Update()
        {
            bool justStartedClicking = false;
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                _mouseLdown = true;
                justStartedClicking = true;

                Tile tile = CheckForTile();

                if (tile != null)
                {
                    //SelectTile(tile);
                    _lastMousePosition = _initialMousePosition = Input.mousePosition;
                    _currentSelectedTile = _firstSelectedTile = tile;
                    _currentSelectedTool.OnTileClick(tile);
                    if (tile.Type != TileType.Void)
                        _currentSelectedTool.OnTileScrollStart(tile, _initialMousePosition);
                }
            }

            // Handle end
            if (_mouseLdown && Input.GetKeyUp(KeyCode.Mouse0))
            {
                _mouseLdown = false;
                _lastDraggedTile = null;
                _firstSelectedTile = null;
                _currentSelectedTile = null;
                return;
            }

            // Handle tile scroll
            if (_firstSelectedTile != null && _firstSelectedTile.Type != TileType.Void && !justStartedClicking &&
                _mouseLdown)
            {
                if (Input.mousePosition != _lastMousePosition)
                {
                    _lastMousePosition = Input.mousePosition;
                    _currentSelectedTool.OnTileScroll(_firstSelectedTile, _lastMousePosition);
                }
            }


            // Handle Tile Drag
            if (!justStartedClicking && _mouseLdown)
            {
                Tile tile = CheckForTile();

                if (tile != null)
                {
                    _currentSelectedTile = _lastDraggedTile = tile;
                    _currentSelectedTool.OnTileDrag(_lastDraggedTile);
                }
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
            if (_currentSelectedTile == tile)
            {
                return;
            }

            if (_currentSelectedTile != null && _currentSelectedTile != tile)
            {
                DeselectTile();
            }

            _currentSelectedTile = tile;
            var mesh = _currentSelectedTile.GetComponent<MeshRenderer>();
            mesh.material.color = Color.green;
        }

        public void DeselectTile()
        {
            var mesh = _currentSelectedTile.GetComponent<MeshRenderer>();
            mesh.material.color = Color.white;
            _currentSelectedTile = null;
        }

        public void SetToolBrush()
        {
            this._currentSelectedTool = _toolBrush;
        }

        public void SetToolEraser()
        {
            this._currentSelectedTool = _toolEraser;
        }

        public void SetToolHeight()
        {
            this._currentSelectedTool = _toolHeight;
        }

        public void SetBrush()
        {
            var tileType = (TileType) BrushDropdown.value;
            _toolBrush.selectedType = tileType;
        }
    }
}