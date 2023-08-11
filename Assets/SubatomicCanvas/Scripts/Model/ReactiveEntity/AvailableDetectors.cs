using UniRx;

namespace SubatomicCanvas.Model
{
    public class AvailableDetectors
    {
        public IReadOnlyReactiveDictionary<string, Detector> DetectorDict => _detectorDict;
        
        private readonly ReactiveDictionary<string, Detector> _detectorDict = new();

        public void Add(string key, Detector detector) => _detectorDict.Add(key, detector);
    }
}