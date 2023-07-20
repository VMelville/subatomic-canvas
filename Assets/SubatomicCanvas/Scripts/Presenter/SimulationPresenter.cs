using ParticleSim.Result;
using SubatomicCanvas.Model;
using SubatomicCanvas.Model.UseCase;
using SubatomicCanvas.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SimulationPresenter : IStartable
    {
        // Model - ReactiveEntity
        [Inject] private AvailableDetectors _availableDetectors;
        [Inject] private AvailableParticles _availableParticles;
        [Inject] private CanvasState _canvasState;
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private ModeState _modeState;
        
        // Model - UseCase
        [Inject] private SimulationUseCase _simulationUseCase;
        
        // View
        [Inject] private SimulatorView _simulatorView;
        
        public void Start()
        {
            _simulatorView.onClick.AddListener(OnClickRunButton);
        }

        private void OnClickRunButton()
        {
            var result = _simulationUseCase.RunSimulation();

            _lastSimulationCondition.result.Value = result;
            Debug.LogWarning("ToDo: 直近に行ったシミュレーションの前提データは残しておく");
            Debug.LogWarning("ToDo: simulatorViewのテキストの変更を行う");
        }
    }
}