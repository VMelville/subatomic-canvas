using UnityEngine;

namespace SubatomicCanvas.Utility.Tween
{
    public static class CoroutineHandler
    {
        private class CoroutineHolder : MonoBehaviour { }

        private static CoroutineHolder holder;

        public static MonoBehaviour GetCoroutineHolder()
        {
            if (holder != null) return holder;
            
            var go = new GameObject("Static Coroutine Holder");
            GameObject.DontDestroyOnLoad(go);
            holder = go.AddComponent<CoroutineHolder>();

            return holder;
        }
    }
}
