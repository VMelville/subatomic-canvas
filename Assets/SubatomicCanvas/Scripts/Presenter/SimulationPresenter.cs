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

            Debug.LogWarning("ToDo: シミュレーション結果の可視化を行う");
            Debug.LogWarning("ToDo: simulatorViewのテキストの変更を行う");
        }

        private void OnClickRunButton()
        {
            var result = _simulationUseCase.RunSimulation(_canvasState.canvasData.Value);

            _lastSimulationCondition.result.Value = result;
            _lastSimulationCondition.canvasData.Value = _canvasState.canvasData.Value;
        }
    }
}