using UniRx;

namespace SubatomicCanvas.Model
{
    public class CameraState
    {
        public Vector3ReactiveProperty position = new();
        public QuaternionReactiveProperty rotation = new();
        public BoolReactiveProperty orthographic = new();
        public FloatReactiveProperty orthographicSize = new();
    }
}