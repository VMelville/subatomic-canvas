using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class PaintToolPresenter : IStartable
    {
        [Inject] private PaintToolState _paintToolState;
        [Inject] private PaintToolView _paintToolView;
        [Inject] private AvailableDetectors _availableDetectors;
        
        public void Start()
        {
            _paintToolState.activeDetectorKey.Subscribe(_paintToolView.SetDetectorKey);
            _paintToolState.isActiveSymmetry.Subscribe(_paintToolView.SetActiveSymmetry);
            
            _paintToolView.OnClickPaintToolButton.AddListener(OnClickPaintTool);
            _paintToolView.OnClickSymmetryModeButton.AddListener(OnClickSymmetryToolButton);
        }

        private void OnClickPaintTool(string toolKey)
        {
            _paintToolState.activeDetectorKey.Value = toolKey;
        }

        private void OnClickSymmetryToolButton()
        {
            _paintToolState.isActiveSymmetry.Value ^= true;
        }
    }
}