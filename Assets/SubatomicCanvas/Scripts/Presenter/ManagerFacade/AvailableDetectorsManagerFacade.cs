using SubatomicCanvas.Model;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class AvailableDetectorsManagerFacade : IStartable
    {
        [Inject] private AvailableDetectorsManager _manager;

        public void Start()
        {
            var trackDetector = new Detector();
            var calorimeter = new Detector();
            var absorber = new Detector();
            
            _manager.Add("TrackDetector", trackDetector);
            _manager.Add("Calorimeter", calorimeter);
            _manager.Add("Absorber", absorber);
        }
    }
}