using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SimulationPresenter : IStartable
    {
        // Model
        [Inject] private AvailableDetectors _availableDetectors;
        [Inject] private AvailableParticles _availableParticles;
        [Inject] private CanvasState _canvasState;
        [Inject] private GlobalSettingState _globalSettingState;
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private TimeState _timeState;

        [Inject] private SampleDetectors _sampleDetectors;
        [Inject] private SimulationService _simulationService;

        // View
        [Inject] private SimulatorView _simulatorView;
        
        public void Start()
        {
            _simulatorView.OnClick.AddListener(OnClickRunButton);
            _globalSettingState.IsDisplayParticleName.Subscribe(_simulatorView.SetDisplayParticleName);
        }

        private void OnClickRunButton()
        {
            // Detector
            var detectorTable = _sampleDetectors.GetDetectorLogicalVolumeTable(_canvasState.CellSize.Value, _canvasState.SimulationWorldDepth.Value);

            // Particle
            var randomParticleKey = ParticleUtil.GetPickedUpParticleKey(_canvasState.UsingParticleKeys);
            if (randomParticleKey == "")
            {
                _simulatorView.SetText("Please select at least one particle.");
                return;
            }
            
            var randomEnergy = ParticleUtil.GetPickedUpEnergy(_canvasState.ParticleEnergyMin.Value, _canvasState.ParticleEnergyMax.Value);
            
            var particleGun = ParticleUtil.MakeParticleGun(randomParticleKey, randomEnergy);
            
            var particleName = _availableParticles.ParticleDict[randomParticleKey].displayName;
            _simulatorView.SetText(particleName);
            
            // シミュレーション実行
            var (result, positionPathDict) = _simulationService.RunSimulation(
                _canvasState.GetDetectorPlacements(),
                detectorTable,
                particleGun,
                _canvasState.SimulationWorldDepth.Value,
                _canvasState.MagneticFieldVector.Value,
                _canvasState.CanvasSize.Value,
                _canvasState.CellSize.Value
                );

            // 結果を記録
            _lastSimulationCondition.SetResult(result, positionPathDict);
            _lastSimulationCondition.SetParticleKey(randomParticleKey);
            _lastSimulationCondition.SetDetectorKeyDict(_canvasState.InstalledDetectorPositionAndKeys);

            _timeState.OnSimulationCompleted();
        }
    }
}