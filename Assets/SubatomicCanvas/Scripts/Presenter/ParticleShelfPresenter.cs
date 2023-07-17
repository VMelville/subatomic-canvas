using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class ParticleShelfPresenter : IStartable
    {
        // Model
        [Inject] private AvailableParticles _availableParticles;
        [Inject] private CanvasState _canvasState;
        
        // View
        [Inject] private ParticleShelfView _particleShelfView;

        public void Start()
        {
            Debug.LogWarning("ToDo: パーティクル選択画面の実装");
        }
    }
}