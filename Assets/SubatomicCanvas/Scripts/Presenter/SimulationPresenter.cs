using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UnityEngine;
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
        [Inject] private SimulationResult _simulationResult;
        [Inject] private ModeState _modeState;
        
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
            Debug.LogWarning("ToDo: シミュレーションの実行を行う");
        }
    }
}