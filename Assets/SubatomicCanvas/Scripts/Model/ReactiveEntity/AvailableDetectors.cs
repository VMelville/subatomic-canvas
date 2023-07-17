using UniRx;

namespace SubatomicCanvas.Model
{
    public class AvailableDetectors
    {
        public readonly ReactiveDictionary<string, Detector> detectorDict = new();
    }
}