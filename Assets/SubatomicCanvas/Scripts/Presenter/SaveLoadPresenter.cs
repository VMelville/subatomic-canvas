using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SaveLoadPresenter : IStartable
    {
        [Inject] private AvailableCanvasDataFiles _availableCanvasDataFiles;
        [Inject] private SaveLoadView _saveLoadView;
        [Inject] private CanvasState _canvasState;
        
        public void Start()
        {
            // ToDo: 全部消した場合ってその回数Remove呼ばれる？ 確認
            _availableCanvasDataFiles.canvasDataFiles.ObserveAdd().Subscribe(OnAddCanvasDataFiles);
            _availableCanvasDataFiles.canvasDataFiles.ObserveRemove().Subscribe(OnRemoveCanvasDataFiles);
            _availableCanvasDataFiles.canvasDataFiles.ObserveReplace().Subscribe(OnReplaceCanvasDataFiles);
            
            Debug.LogWarning("ToDo: UIの入力を受けてキャンバスデータをセーブする。");
            Debug.LogWarning("ToDo: UIの入力を受けてキャンバスデータをロードする。");
        }

        private void OnAddCanvasDataFiles(CollectionAddEvent<CanvasDataFileInfo> addEvent)
        {
            Debug.LogWarning("ToDo: OnAddCanvasDataFiles");
        }
        
        private void OnRemoveCanvasDataFiles(CollectionRemoveEvent<CanvasDataFileInfo> removeEvent)
        {
            Debug.LogWarning("ToDo: OnRemoveCanvasDataFiles");
        }
        
        private void OnReplaceCanvasDataFiles(CollectionReplaceEvent<CanvasDataFileInfo> replaceEvent)
        {
            Debug.LogWarning("ToDo: OnReplaceCanvasDataFiles");
        }
    }
}