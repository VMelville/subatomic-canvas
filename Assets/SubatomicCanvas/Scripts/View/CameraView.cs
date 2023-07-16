using UnityEngine;

namespace SubatomicCanvas.View
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        public void SetPosition(Vector3 position)
        {
            cam.transform.position = position;
        }
        
        public void SetRotation(Quaternion rotation)
        {
            cam.transform.rotation = rotation;
        }
        
        public void SetOrthographic(bool orthographic)
        {
            cam.orthographic = orthographic;
        }
        
        public void SetOrthographicSize(float orthographicSize)
        {
            cam.orthographicSize = orthographicSize;
        }
    }
}