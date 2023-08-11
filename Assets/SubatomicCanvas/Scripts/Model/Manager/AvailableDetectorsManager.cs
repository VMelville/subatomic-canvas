using System.Collections.Generic;

namespace SubatomicCanvas.Model
{
    public class AvailableDetectorsManager
    {
        public IReadOnlyDictionary<string, Detector> DetectorDict => _detectorDict;

        private readonly Dictionary<string, Detector> _detectorDict = new();

        public void Add(string key, Detector detector) => _detectorDict.Add(key, detector);
    }
}