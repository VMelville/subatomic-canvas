using UniRx;

namespace SubatomicCanvas.Model
{
    public class CameraState
    {
        public Vector3ReactiveProperty position = new Vector3ReactiveProperty();
        public QuaternionReactiveProperty rotation = new QuaternionReactiveProperty();
        public BoolReactiveProperty orthographic = new BoolReactiveProperty();
        public FloatReactiveProperty orthographicSize = new FloatReactiveProperty();
    }
}