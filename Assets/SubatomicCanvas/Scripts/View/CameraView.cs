using UnityEngine;

namespace SubatomicCanvas.View
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        public void UpdateCamera(Vector2 position, float zoom)
        {
            var camTransform = cam.transform;
            camTransform.position = new Vector3(0.0f, 0.0f, -10.0f) - (Vector3)position;
            camTransform.rotation = new Quaternion(); // 一旦角度はいじらない
            cam.orthographicSize = Screen.height / 2f / zoom * 0.001f;
        }
        
        public void SetOrthographic(bool orthographic)
        {
            cam.orthographic = orthographic;
        }
    }
}