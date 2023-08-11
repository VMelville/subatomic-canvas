namespace SubatomicCanvas.Model
{
    public abstract class SafeParseUtil
    {
        public static int SafeParseInt(string str, int defaultValue)
        {
            return int.TryParse(str, out var value) ? value : defaultValue;
        }

        public static float SafeParseFloat(string str, float defaultValue)
        {
            return float.TryParse(str, out var value) ? value : defaultValue;
        }
    }
}