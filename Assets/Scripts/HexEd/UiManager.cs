using HexEd;

namespace Assets.Scripts.HexEd
{
    public class UiManager : Singleton<UiManager>
    {
        private CameraHandler camera;

        public CameraHandler Camera
        {
            get {
                if (camera == null)
                {
                    camera = FindObjectOfType<CameraHandler>();
                }
                return camera;
            }
        }

        public TileSelectionManager tileSelectionManager;
        public TileSelectionManager TileSelectionManager
        {
            get
            {
                if (tileSelectionManager == null)
                {
                    tileSelectionManager = FindObjectOfType<TileSelectionManager>();
                }
                return tileSelectionManager;
            }
        }

        public void OnNewButtonClicked()
        {
            //MapManager.Instance.CleanMap();
            //MapManager.Instance.GenerateEmptyMap(2,4);
        }
    }
}