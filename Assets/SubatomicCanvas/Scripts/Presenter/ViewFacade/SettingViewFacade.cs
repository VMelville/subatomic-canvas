using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SettingViewFacade : IStartable
    {
        [Inject] private SettingView _view;
        
        [Inject] private GlobalSettingManager _globalSettingManager;
        [Inject] private CanvasManager _canvasManager;

        public void Start()
        {
            _canvasManager.CanvasSize.Subscribe(_view.SetCanvasSize);
            _canvasManager.CellSize.Subscribe(_view.SetCellSize);
            _canvasManager.SimulationWorldDepth.Subscribe(_view.SetSimulationWorldDepth);
            _canvasManager.ParticleEnergyMin.Subscribe(_view.SetParticleEnergyMin);
            _canvasManager.ParticleEnergyMax.Subscribe(_view.SetParticleEnergyMax);
            _canvasManager.MagneticFieldVector.Subscribe(_view.SetMagneticField);
            _globalSettingManager.IsDisplayParticleName.Subscribe(_view.SetIsDisplayParticleName);
            _globalSettingManager.IsDisplayLineVisualizer.Subscribe(_view.SetIsDisplayLineVisualizer);
        }
    }
}