using System.Linq;

namespace SubatomicCanvas.Model
{
    public static class PositionTupleExtension
    {
        public static (int, int) ToTuple(this string str)
        {
            var modified = str.Trim(new char[] {'(', ')'});
            var parts = modified.Split(',').Select(int.Parse).ToArray();
            return (parts[0], parts[1]);
        }
    }
}