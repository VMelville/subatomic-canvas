using System;
using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public class CameraManager : IDisposable
    {
        public IReadOnlyReactiveProperty<Vector2> Position => _position;
        public IReadOnlyReactiveProperty<float> ZoomLevel => _zoomLevel;
        public IReadOnlyReactiveProperty<bool> Is2dView => _is2dView;

        private readonly Vector2ReactiveProperty _position = new();
        private readonly FloatReactiveProperty _zoomLevel = new(1.0f);
        private readonly BoolReactiveProperty _is2dView = new(true);

        public void Grab(Vector2 delta)
        {
            _position.Value += delta * 0.001f / _zoomLevel.Value;
        }

        public void Zoom(float zoom)
        {
            _zoomLevel.Value *= 1f + zoom * 0.1f;
        }

        public void Dispose()
        {
            _position?.Dispose();
            _zoomLevel?.Dispose();
            _is2dView?.Dispose();
        }
    }
}