using System.Collections.Generic;
using UnityEngine;

namespace SubatomicCanvas.Util
{
    public static class HoneycombCoordinate
    {
        public static Vector2 GetPosition((int, int) position, float cellSize)
        {
            var (x, y) = position;
            return new Vector2(x * 1.5f, (x * 0.5f - y) * Mathf.Sqrt(3.0f)) * 0.5f * cellSize;
        }
        
        // ToDo: これintかfloatかで分けてるけど、なんとなく微妙じゃない？
        public static Vector2 GetPosition(float x, float y, float cellSize)
        {
            return new Vector2(x * 1.5f, (x * 0.5f - y) * Mathf.Sqrt(3.0f)) * 0.5f * cellSize;
        }

        public static List<(int, int)> MakeSubCursorPosition((int, int) position)
        {
            var (j, i) = position;
            return new List<(int, int)> {(j - i, j), (-i, j - i), (-j, -i), (-j + i, -j), (i, -j + i)};
        }
    }
}
