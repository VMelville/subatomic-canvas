using UniRx;

namespace SubatomicCanvas.Model
{
    public class CameraState
    {
        public Vector3ReactiveProperty cameraPosition3D;
        public QuaternionReactiveProperty cameraRotation3D;
        public Vector2ReactiveProperty cameraPosition2D;
        public FloatReactiveProperty orthoSize;
    }
}