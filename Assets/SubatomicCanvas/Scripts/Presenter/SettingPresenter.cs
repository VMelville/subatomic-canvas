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
        [Inject] private GlobalSettingState _globalSettingState;
        [Inject] private CanvasState _canvasState;

        // View
        [Inject] private SettingView _settingView;

        public void Start()
        {
            _canvasState.CanvasSize.Subscribe(_settingView.SetCanvasSize);
            _canvasState.CellSize.Subscribe(_settingView.SetCellSize);
            _canvasState.SimulationWorldDepth.Subscribe(_settingView.SetSimulationWorldDepth);
            _canvasState.ParticleEnergyMin.Subscribe(_settingView.SetParticleEnergyMin);
            _canvasState.ParticleEnergyMax.Subscribe(_settingView.SetParticleEnergyMax);
            _canvasState.MagneticFieldVector.Subscribe(_settingView.SetMagneticField);
            _globalSettingState.IsDisplayParticleName.Subscribe(_settingView.SetIsDisplayParticleName);
            _globalSettingState.IsDisplayLineVisualizer.Subscribe(_settingView.SetIsDisplayLineVisualizer);

            _settingView.OnEndEditCanvasSize.AddListener(s => _canvasState.SetCanvasSize(SafeParseUtil.SafeParseInt(s, 10)));
            _settingView.OnEndEditCellSize.AddListener(s => _canvasState.SetCellSize(SafeParseUtil.SafeParseFloat(s, 0.1f)));
            _settingView.OnEndEditSimulationWorldDepth.AddListener(s => _canvasState.SetSimulationWorldDepth(SafeParseUtil.SafeParseFloat(s, 10f)));
            _settingView.OnEndEditParticleEnergyMin.AddListener(s => _canvasState.TrySetParticleEnergyMin(SafeParseUtil.SafeParseFloat(s, 100f)));
            _settingView.OnEndEditParticleEnergyMax.AddListener(s => _canvasState.TrySetParticleEnergyMax(SafeParseUtil.SafeParseFloat(s, 300f)));
            _settingView.OnEndEditMagneticFieldX.AddListener(s => _canvasState.SetMagneticFieldX(SafeParseUtil.SafeParseFloat(s, 0f)));
            _settingView.OnEndEditMagneticFieldY.AddListener(s => _canvasState.SetMagneticFieldY(SafeParseUtil.SafeParseFloat(s, 0f)));
            _settingView.OnEndEditMagneticFieldZ.AddListener(s => _canvasState.SetMagneticFieldZ(SafeParseUtil.SafeParseFloat(s, 0f)));
            _settingView.OnToggleDisplayParticleName.AddListener(isOn => _globalSettingState.SetIsDisplayParticleName(isOn));
            _settingView.OnToggleDisplayLineVisualizer.AddListener(isOn => _globalSettingState.SetIsDisplayLineVisualizer(isOn));
        }
    }
}