using SubatomicCanvas.Model;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class DefaultSettingsPresenter : IStartable
    {
        [Inject] private AvailableDetectors _availableDetectors;
        [Inject] private AvailableParticles _availableParticles;

        public void Start()
        {
            var trackDetector = new Detector();
            var calorimeter = new Detector();
            var absorber = new Detector();
            
            _availableDetectors.detectorDict.Add("TrackDetector", trackDetector);
            _availableDetectors.detectorDict.Add("Calorimeter", calorimeter);
            _availableDetectors.detectorDict.Add("Absorber", absorber);
        }
    }
}