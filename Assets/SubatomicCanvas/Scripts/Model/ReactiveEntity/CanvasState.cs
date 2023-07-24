using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public class CanvasState
    {
        public ReactiveCollection<string> usingParticleKeys = new();
        public ReactiveDictionary<(int, int), string> installedDetectorPositionAndKeys = new();
        public Vector3ReactiveProperty magneticFieldVector = new(new Vector3(0f, 0f, 0.5f));
        public Vector3ReactiveProperty simulationWorldScale = new(new Vector3(8f, 8f, 5f));
        public FloatReactiveProperty cellSize = new(0.1f); // 単位は m
        public IntReactiveProperty canvasSize = new(0); // 六角形1辺あたりのセル数
        public FloatReactiveProperty particleEnergyMax = new(300.0f); // 単位は MeV
        public FloatReactiveProperty particleEnergyMin = new(100.0f); // 単位は MeV
    }
}