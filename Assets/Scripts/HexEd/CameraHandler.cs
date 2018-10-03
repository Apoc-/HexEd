using System;
using UnityEngine;

namespace Assets.Scripts.HexEd
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private float panSpeed = 0.5f;

        [SerializeField] private float zoomMin = 2.5f;
        [SerializeField] private float zoomMax = 10.0f;
        [SerializeField] private float rotXMin = 20.0f;
        [SerializeField] private float rotXMax = 90.0f;
        [SerializeField] private float zoomSpeed = 1.5f;

        private void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var zoomInput = Input.GetAxis("Mouse ScrollWheel");
            var dead = 0.1;

            horizontalInput = (Math.Abs(horizontalInput) > dead) ? horizontalInput : 0;
            verticalInput = (Math.Abs(verticalInput) > dead) ? verticalInput : 0;

            PanCamera(horizontalInput, verticalInput);
            if (Math.Abs(zoomInput) > 0)
            {
                ZoomCamera(zoomInput);
            }
        }

        public void CenterCameraToMap(Map map)
        {
            var cam = GetComponent<Camera>();

            var newPosition = new Vector3()
            {
                x = map.Extents.LeftBound + map.Extents.Width / 2.0f,
                y = this.zoomMax,
                z = map.Extents.UpperBound - map.Extents.Height / 2.0f
            };

            var eulerAngles = cam.transform.localEulerAngles;
            eulerAngles.x = rotXMax;
            cam.transform.rotation = Quaternion.Euler(eulerAngles);

            cam.transform.position = newPosition;
        }

        private void ZoomCamera(float zoomInput)
        {
            var cam = GetComponent<Camera>();
            var zoom = -zoomInput * zoomSpeed;

            transform.Translate(0, zoom, 0, Space.World);
            var pos = transform.position;
            var y = Mathf.Clamp(transform.position.y, zoomMin, zoomMax);
            transform.position = new Vector3(pos.x, y, pos.z);

            var zoomVal = transform.position.y;
            var p = (zoomVal - zoomMin) / (zoomMax - zoomMin);
            var rotVal = (rotXMax - rotXMin) * p + rotXMin;
            var euler = transform.localEulerAngles;
            euler.x = rotVal;

            transform.rotation = Quaternion.Euler(euler);
        }

        private void PanCamera(float horizontalInput, float verticalInput)
        {
            gameObject.transform.Translate(Vector3.right * horizontalInput * panSpeed);

            var pos = gameObject.transform.position;
            pos.z += verticalInput * panSpeed;

            /**
            //clamp movement to map extends
            var upper = MapManager.Instance.Map.Extents.UpperBound;
            var left = MapManager.Instance.Map.Extents.LeftBound;
            var height = MapManager.Instance.Map.Extents.Height;
            var width = MapManager.Instance.Map.Extents.Width;

            if (pos.x < left) pos.x = left;
            if (pos.x > left + width) pos.x = left + width;

            if (pos.z > upper) pos.z = upper;
            if (pos.z < upper - height) pos.z = upper - height;
*/
            gameObject.transform.position = pos;
        }
    }
}
