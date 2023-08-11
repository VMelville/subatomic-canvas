using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SettingViewFacade : ControllerBase, IStartable
    {
        [Inject] private SettingView _view;
        
        [Inject] private GlobalSettingManager _globalSettingManager;
        [Inject] private CanvasManager _canvasManager;

        public void Start()
        {
            _canvasManager.CanvasSize
                .Subscribe(_view.SetCanvasSize)
                .AddTo(this);
            
            _canvasManager.CellSize
                .Subscribe(_view.SetCellSize)
                .AddTo(this);
            
            _canvasManager.SimulationWorldDepth
                .Subscribe(_view.SetSimulationWorldDepth)
                .AddTo(this);
            
            _canvasManager.ParticleEnergyMin
                .Subscribe(_view.SetParticleEnergyMin)
                .AddTo(this);
            
            _canvasManager.ParticleEnergyMax
                .Subscribe(_view.SetParticleEnergyMax)
                .AddTo(this);
            
            _canvasManager.MagneticFieldVector
                .Subscribe(_view.SetMagneticField)
                .AddTo(this);
            
            _globalSettingManager.IsDisplayParticleName
                .Subscribe(_view.SetIsDisplayParticleName)
                .AddTo(this);
            
            _globalSettingManager.IsDisplayLineVisualizer
                .Subscribe(_view.SetIsDisplayLineVisualizer)
                .AddTo(this);
        }
    }
}