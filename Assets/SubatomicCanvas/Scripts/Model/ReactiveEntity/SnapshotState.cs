using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    /// <summary>
    /// スナップショットにまつわる状態管理
    /// </summary>
    public class SnapshotState
    {
        public IReadOnlyReactiveProperty<SnapshotStateType> State => _state;
        
        private readonly ReactiveProperty<SnapshotStateType> _state = new(SnapshotStateType.NormalTime);

        public void DoSnapshot()
        {
            if (_state.Value == SnapshotStateType.NormalTime)
            {
                _state.Value = SnapshotStateType.PrePare;
            }
            else
            {
                Debug.LogWarning("スナップショットボタンが押されましたが、スナップショット撮影処理中のため、無視します。");
            }
        }

        public void OnSetActiveUi(bool isActive)
        {
            if (isActive)
            {
                if (_state.Value == SnapshotStateType.Took)
                {
                    // UIのアクティブ化を確認したので「NormalTime」へ移行して制御を終了
                    _state.Value = SnapshotStateType.NormalTime;
                }
                else
                {
                    Debug.LogError("想定されていない制御フローが行われている可能性があります。\nUIがアクティブ化される前にStateはTookに移行する必要があります。");
                }
            }
            else
            {
                if (_state.Value == SnapshotStateType.PrePare)
                {
                    // UIの非アクティブ化を確認したので「Standby」へ移行
                    _state.Value = SnapshotStateType.Standby;
                }
                else
                {
                    Debug.LogError("想定されていない制御フローが行われている可能性があります。\nUIが非アクティブ化される前にStateはPrePareに移行する必要があります。");
                }
            }
        }

        public void OnTookSnapshot()
        {
            if (_state.Value == SnapshotStateType.Standby)
            {
                _state.Value = SnapshotStateType.Took;
            }
            else
            {
                Debug.LogError("「Standby」状態にないにも関わらずスナップショットが撮影されました。制御フローに誤りがある可能性があります。");
            }
        }
    }
}