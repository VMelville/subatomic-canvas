using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SettingPresenter : IStartable
    {
        // Model
        [Inject] private GlobalSettingManager _globalSettingManager;
        [Inject] private CanvasManager _canvasManager;

        // View
        [Inject] private SettingView _settingView;

        public void Start()
        {
            _canvasManager.CanvasSize.Subscribe(_settingView.SetCanvasSize);
            _canvasManager.CellSize.Subscribe(_settingView.SetCellSize);
            _canvasManager.SimulationWorldDepth.Subscribe(_settingView.SetSimulationWorldDepth);
            _canvasManager.ParticleEnergyMin.Subscribe(_settingView.SetParticleEnergyMin);
            _canvasManager.ParticleEnergyMax.Subscribe(_settingView.SetParticleEnergyMax);
            _canvasManager.MagneticFieldVector.Subscribe(_settingView.SetMagneticField);
            _globalSettingManager.IsDisplayParticleName.Subscribe(_settingView.SetIsDisplayParticleName);
            _globalSettingManager.IsDisplayLineVisualizer.Subscribe(_settingView.SetIsDisplayLineVisualizer);

            _settingView.OnEndEditCanvasSize.AddListener(s => _canvasManager.SetCanvasSize(SafeParseUtil.SafeParseInt(s, 10)));
            _settingView.OnEndEditCellSize.AddListener(s => _canvasManager.SetCellSize(SafeParseUtil.SafeParseFloat(s, 0.1f)));
            _settingView.OnEndEditSimulationWorldDepth.AddListener(s => _canvasManager.SetSimulationWorldDepth(SafeParseUtil.SafeParseFloat(s, 10f)));
            _settingView.OnEndEditParticleEnergyMin.AddListener(s => _canvasManager.TrySetParticleEnergyMin(SafeParseUtil.SafeParseFloat(s, 100f)));
            _settingView.OnEndEditParticleEnergyMax.AddListener(s => _canvasManager.TrySetParticleEnergyMax(SafeParseUtil.SafeParseFloat(s, 300f)));
            _settingView.OnEndEditMagneticFieldX.AddListener(s => _canvasManager.SetMagneticFieldX(SafeParseUtil.SafeParseFloat(s, 0f)));
            _settingView.OnEndEditMagneticFieldY.AddListener(s => _canvasManager.SetMagneticFieldY(SafeParseUtil.SafeParseFloat(s, 0f)));
            _settingView.OnEndEditMagneticFieldZ.AddListener(s => _canvasManager.SetMagneticFieldZ(SafeParseUtil.SafeParseFloat(s, 0f)));
            _settingView.OnToggleDisplayParticleName.AddListener(isOn => _globalSettingManager.SetIsDisplayParticleName(isOn));
            _settingView.OnToggleDisplayLineVisualizer.AddListener(isOn => _globalSettingManager.SetIsDisplayLineVisualizer(isOn));
        }
    }
}