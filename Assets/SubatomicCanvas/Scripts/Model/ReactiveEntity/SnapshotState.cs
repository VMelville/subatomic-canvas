using UniRx;
namespace SubatomicCanvas.Model
{
    /// <summary>
    /// スナップショットにまつわる状態管理
    /// </summary>
    public class SnapshotState
    {
        public ReactiveProperty<SnapshotStateType> state = new(SnapshotStateType.NormalTime);
    }
}