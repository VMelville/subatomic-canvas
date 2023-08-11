using System;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public static class SafeParseUtil
    {
        public static int SafeParseInt(string str, int defaultValue, int min = int.MinValue, int max = int.MaxValue)
        {
            return int.TryParse(str, out var value) ? Mathf.Clamp(value, min, max) : defaultValue;
        }

        public static float SafeParseFloat(string str, float defaultValue, float min = float.MinValue, float max = float.MaxValue)
        {
            return float.TryParse(str, out var value) ? Mathf.Clamp(value, min, max) : defaultValue;
        }
    }
}