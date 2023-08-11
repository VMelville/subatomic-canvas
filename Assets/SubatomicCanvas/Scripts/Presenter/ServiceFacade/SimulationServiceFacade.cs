using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SimulationServiceFacade : IStartable
    {
        [Inject] private SimulationService _service;
        
        [Inject] private CanvasManager _canvasManager;
        [Inject] private SimulatorView _simulatorView;
        
        public void Start()
        {
            _simulatorView.OnClick.AddListener(() =>
            {
                // Detector
                var detectorTable = SampleDetectors.GetDetectorLogicalVolumeTable(_canvasManager.CellSize.Value, _canvasManager.SimulationWorldDepth.Value);

                // Particle - Name
                var randomParticleKey = ParticleUtil.GetPickedUpParticleKey(_canvasManager.UsingParticleKeys);

                if (randomParticleKey == "") return;

                var randomEnergy = ParticleUtil.GetPickedUpEnergy(_canvasManager.ParticleEnergyMin.Value, _canvasManager.ParticleEnergyMax.Value);

                var particleGun = ParticleUtil.MakeParticleGun(randomParticleKey, randomEnergy);
                
                // シミュレーション実行
                _service.RunSimulation(
                    _canvasManager.GetDetectorPlacements(),
                    detectorTable,
                    particleGun,
                    _canvasManager.SimulationWorldDepth.Value,
                    _canvasManager.MagneticFieldVector.Value,
                    _canvasManager.CanvasSize.Value,
                    _canvasManager.CellSize.Value
                );
            });
        }
    }
}