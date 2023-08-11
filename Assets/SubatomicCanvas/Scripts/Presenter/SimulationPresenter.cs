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
        [Inject] private CanvasManager _canvasManager;
        [Inject] private GlobalSettingManager _globalSettingManager;
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private TimeManager _timeManager;

        [Inject] private SampleDetectors _sampleDetectors;
        [Inject] private SimulationService _simulationService;

        // View
        [Inject] private SimulatorView _simulatorView;
        
        public void Start()
        {
            _simulatorView.OnClick.AddListener(() =>
            {
                // Detector
                var detectorTable = _sampleDetectors.GetDetectorLogicalVolumeTable(_canvasManager.CellSize.Value,
                    _canvasManager.SimulationWorldDepth.Value);

                // Particle
                var randomParticleKey = ParticleUtil.GetPickedUpParticleKey(_canvasManager.UsingParticleKeys);
                if (randomParticleKey == "")
                {
                    _simulatorView.SetText("Please select at least one particle.");
                    return;
                }

                var randomEnergy = ParticleUtil.GetPickedUpEnergy(_canvasManager.ParticleEnergyMin.Value,
                    _canvasManager.ParticleEnergyMax.Value);

                var particleGun = ParticleUtil.MakeParticleGun(randomParticleKey, randomEnergy);

                var particleName = _availableParticles.ParticleDict[randomParticleKey].displayName;
                _simulatorView.SetText(particleName);

                // シミュレーション実行
                var (result, positionPathDict) = _simulationService.RunSimulation(
                    _canvasManager.GetDetectorPlacements(),
                    detectorTable,
                    particleGun,
                    _canvasManager.SimulationWorldDepth.Value,
                    _canvasManager.MagneticFieldVector.Value,
                    _canvasManager.CanvasSize.Value,
                    _canvasManager.CellSize.Value
                );

                // 結果を記録
                _lastSimulationConditionManager.SetResult(result, positionPathDict);
                _lastSimulationConditionManager.SetParticleKey(randomParticleKey);
                _lastSimulationConditionManager.SetDetectorKeyDict(_canvasManager.InstalledDetectorPositionAndKeys);

                _timeManager.OnSimulationCompleted();
            });
            
            _globalSettingManager.IsDisplayParticleName.Subscribe(_simulatorView.SetDisplayParticleName);
        }
    }
}