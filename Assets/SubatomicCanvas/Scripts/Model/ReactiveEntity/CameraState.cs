using UniRx;

namespace SubatomicCanvas.Model
{
    public class CameraState
    {
        // public Vector3ReactiveProperty position = new(new Vector3(0f, 0f, -10f));
        // public QuaternionReactiveProperty rotation = new();
        public Vector2ReactiveProperty position = new();
        public FloatReactiveProperty zoomLevel = new(1.0f);
        public BoolReactiveProperty is2dView = new(true);
        // public FloatReactiveProperty orthographicSize = new(0.25f);
    }
}