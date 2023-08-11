using UnityEngine;

namespace SubatomicCanvas.View
{
    public static class EasingFunctions
    {
        public static float Linear(float t)
        {
            return t;
        }
        
        public static float OutCubic(float t)
        {
            return 1.0f - Mathf.Pow(1.0f - t, 3.0f);
        }
        
        public static float OutExpo(float t)
        {
            return t >= 1.0f ? 1.0f : 1.0f - Mathf.Pow(2.0f, -10.0f * t);
        }
    }
}
