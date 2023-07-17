using System.Collections.Generic;
using UnityEngine;

namespace SubatomicCanvas.Utility
{
    public static class HoneycombCoordinate
    {
        private const float Distance = 0.05f;
    
        public static Vector2 GetPosition((int, int) position)
        {
            var (x, y) = position;
            return new Vector2(x * 1.5f, (x * 0.5f - y) * Mathf.Sqrt(3.0f)) * Distance;
        }
        
        // ToDo: これintかfloatかで分けてるけど、なんとなく微妙じゃない？
        public static Vector2 GetPosition(float x, float y)
        {
            return new Vector2(x * 1.5f, (x * 0.5f - y) * Mathf.Sqrt(3.0f)) * Distance;
        }

        public static List<(int, int)> MakeSubCursorPosition((int, int) position)
        {
            var (j, i) = position;
            return new List<(int, int)> {(j - i, j), (-i, j - i), (-j, -i), (-j + i, -j), (i, -j + i)};
        }
    }
}
