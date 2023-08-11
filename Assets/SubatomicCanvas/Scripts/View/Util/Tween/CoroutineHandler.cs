using UnityEngine;

namespace SubatomicCanvas.View
{
    public static class CoroutineHandler
    {
        private class CoroutineHolder : MonoBehaviour { }

        private static CoroutineHolder _holder;

        public static MonoBehaviour GetCoroutineHolder()
        {
            if (_holder != null) return _holder;
            
            var go = new GameObject("Static Coroutine Holder");
            Object.DontDestroyOnLoad(go);
            _holder = go.AddComponent<CoroutineHolder>();

            return _holder;
        }
    }
}
