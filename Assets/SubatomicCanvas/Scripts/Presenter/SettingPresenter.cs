using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SettingPresenter : IStartable
    {
        [Inject] private GlobalSettingState _globalSettingState;
        [Inject] private CanvasState _canvasState;

        [Inject] private SettingView _settingView;

        public void Start()
        {
            _canvasState.canvasSize.Subscribe(_settingView.SetCanvasSize);
            _canvasState.cellSize.Subscribe(_settingView.SetCellSize);
            _canvasState.simulationWorldDepth.Subscribe(_settingView.SetSimulationWorldDepth);
            _canvasState.particleEnergyMin.Subscribe(_settingView.SetParticleEnergyMin);
            _canvasState.particleEnergyMax.Subscribe(_settingView.SetParticleEnergyMax);
            _canvasState.magneticFieldVector.Subscribe(_settingView.SetMagneticField);
            _globalSettingState.isDisplayParticleName.Subscribe(_settingView.SetIsDisplayParticleName);
            _globalSettingState.isDisplayLineVisualizer.Subscribe(_settingView.SetIsDisplayLineVisualizer);

            _settingView.OnEndEditCanvasSize.AddListener(s => _canvasState.canvasSize.Value = SafeParseInt(s, 10));
            _settingView.OnEndEditCellSize.AddListener(s => _canvasState.cellSize.Value = SafeParseFloat(s, 0.1f));
            _settingView.OnEndEditSimulationWorldDepth.AddListener(s => _canvasState.simulationWorldDepth.Value = SafeParseFloat(s, 10f));
            _settingView.OnEndEditParticleEnergyMin.AddListener(s => _canvasState.particleEnergyMin.Value = Mathf.Min(SafeParseFloat(s, 100f), _canvasState.particleEnergyMax.Value));
            _settingView.OnEndEditParticleEnergyMax.AddListener(s => _canvasState.particleEnergyMax.Value = Mathf.Max(SafeParseFloat(s, 300f), _canvasState.particleEnergyMin.Value));
            _settingView.OnEndEditMagneticFieldX.AddListener(s =>
            {
                var mag = _canvasState.magneticFieldVector.Value;
                mag.x = SafeParseFloat(s, 0f);
                _canvasState.magneticFieldVector.Value = mag;
            });
            _settingView.OnEndEditMagneticFieldY.AddListener(s =>
            {
                var mag = _canvasState.magneticFieldVector.Value;
                mag.y = SafeParseFloat(s, 0f);
                _canvasState.magneticFieldVector.Value = mag;
            });
            _settingView.OnEndEditMagneticFieldZ.AddListener(s =>
            {
                var mag = _canvasState.magneticFieldVector.Value;
                mag.z = SafeParseFloat(s, 0f);
                _canvasState.magneticFieldVector.Value = mag;
            });
            _settingView.OnToggleDisplayParticleName.AddListener(isOn => _globalSettingState.isDisplayParticleName.Value = isOn);
            _settingView.OnToggleDisplayLineVisualizer.AddListener(isOn => _globalSettingState.isDisplayLineVisualizer.Value = isOn);
        }
        
        private static int SafeParseInt(string str, int defaultValue)
        {
            return int.TryParse(str, out var value) ? value : defaultValue;
        }
        
        private static float SafeParseFloat(string str, float defaultValue)
        {
            return float.TryParse(str, out var value) ? value : defaultValue;
        }
    }
}