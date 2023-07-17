using UniRx;

namespace SubatomicCanvas.Model
{
    public class SimulationResult
    {
        // ToDo: シミュレーションの可視化に必要な情報を完全に含むようなデータ型を追加する
        
        public ReactiveProperty<CanvasData> lastCanvasData = new();
    }
}