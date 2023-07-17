using System;
using SubatomicCanvas.Model;
using SubatomicCanvas.Utility.Snapshot;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    // ToDo: もっとシンプルに制御できるような気もします。。。
    public class SnapshotPresenter : IStartable
    {
        [Inject] private SnapshotState _snapshotState;
        [Inject] private UiVisibleView _uiVisibleView;
        [Inject] private SnapshotUseCase _snapshotUseCase;
        [Inject] private SnapshotButtonView _snapshotButtonView;
        
        public void Start()
        {
            // Model
            _snapshotState.state.Subscribe(OnChangeState);
            
            _snapshotButtonView.onClick.AddListener(OnClickSnapshotButton);
            _uiVisibleView.onSetActive.AddListener(OnSetActiveUi);
            _snapshotUseCase.onTookSnapshot.AddListener(OnTookSnapshot);
        }

        private void OnChangeState(SnapshotStateType state)
        {
            switch (state)
            {
                case SnapshotStateType.NormalTime:
                    break;
                case SnapshotStateType.PrePare:
                    _uiVisibleView.SetIsVisible(false);
                    break;
                case SnapshotStateType.Standby:
                    _snapshotUseCase.TakeSnapshot();
                    break;
                case SnapshotStateType.Took:
                    _uiVisibleView.SetIsVisible(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        private void OnClickSnapshotButton()
        {
            if (_snapshotState.state.Value == SnapshotStateType.NormalTime)
            {
                _snapshotState.state.Value = SnapshotStateType.PrePare;
            }
            else
            {
                Debug.LogWarning("スナップショットボタンが押されましたが、スナップショット撮影処理中のため、無視します。");
            }
        }

        private void OnSetActiveUi(bool isActive)
        {
            if (isActive)
            {
                if (_snapshotState.state.Value == SnapshotStateType.Took)
                {
                    // UIのアクティブ化を確認したので「NormalTime」へ移行して制御を終了
                    _snapshotState.state.Value = SnapshotStateType.NormalTime;
                }
                else
                {
                    Debug.LogError("想定されていない制御フローが行われている可能性があります。\nUIがアクティブ化される前にStateはTookに移行する必要があります。");
                }
            }
            else
            {
                if (_snapshotState.state.Value == SnapshotStateType.PrePare)
                {
                    // UIの非アクティブ化を確認したので「Standby」へ移行
                    _snapshotState.state.Value = SnapshotStateType.Standby;
                }
                else
                {
                    Debug.LogError("想定されていない制御フローが行われている可能性があります。\nUIが非アクティブ化される前にStateはPrePareに移行する必要があります。");
                }
            }
        }

        private void OnTookSnapshot()
        {
            if (_snapshotState.state.Value == SnapshotStateType.Standby)
            {
                _snapshotState.state.Value = SnapshotStateType.Took;
            }
            else
            {
                Debug.LogError("「Standby」状態にないにも関わらずスナップショットが撮影されました。制御フローに誤りがある可能性があります。");
            }
        }

    }
}