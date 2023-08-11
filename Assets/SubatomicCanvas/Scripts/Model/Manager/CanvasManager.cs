using System.Collections.Generic;
using System.Linq;
using SubatomicCanvas.Utility;
using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public class CanvasManager
    {
        public IReadOnlyReactiveCollection<string> UsingParticleKeys => _usingParticleKeys;
        public IReadOnlyReactiveDictionary<(int, int), string> InstalledDetectorPositionAndKeys => _installedDetectorPositionAndKeys;
        public IReadOnlyReactiveProperty<Vector3> MagneticFieldVector => _magneticFieldVector;
        public IReadOnlyReactiveProperty<float> SimulationWorldDepth => _simulationWorldDepth;
        public IReadOnlyReactiveProperty<float> CellSize => _cellSize;
        public IReadOnlyReactiveProperty<int> CanvasSize => _canvasSize;
        public IReadOnlyReactiveProperty<float> ParticleEnergyMin => _particleEnergyMin;
        public IReadOnlyReactiveProperty<float> ParticleEnergyMax => _particleEnergyMax;

        private readonly ReactiveCollection<string> _usingParticleKeys = new();
        private readonly ReactiveDictionary<(int, int), string> _installedDetectorPositionAndKeys = new();
        private readonly Vector3ReactiveProperty _magneticFieldVector = new(new Vector3(0f, 0f, 0.5f));
        private readonly FloatReactiveProperty _simulationWorldDepth = new(10f); // 単位は m
        private readonly FloatReactiveProperty _cellSize = new(0.1f); // 単位は m
        private readonly IntReactiveProperty _canvasSize = new(0); // 六角形1辺あたりのセル数
        private readonly FloatReactiveProperty _particleEnergyMin = new(100.0f); // 単位は MeV
        private readonly FloatReactiveProperty _particleEnergyMax = new(300.0f); // 単位は MeV

        public void SetCellSize(float cellSize)
        {
            _cellSize.Value = cellSize;
            ClearDetectors();
        }

        public void SetCanvasSize(int canvasSize)
        {
            _canvasSize.Value = canvasSize;
            ClearDetectors();
        }

        public void SetSimulationWorldDepth(float simulationWorldDepth) => _simulationWorldDepth.Value = simulationWorldDepth;
        private void SetMagneticFieldVector(Vector3 magneticField) =>_magneticFieldVector.Value = magneticField;
        private void ClearDetectors() => _installedDetectorPositionAndKeys.Clear();

        private void RegisterDetector((int, int) position, string key) => _installedDetectorPositionAndKeys[position] = key;
        private void UnregisterDetector((int, int) position)=>_installedDetectorPositionAndKeys.Remove(position);
        public Dictionary<(int, int), string> GetDetectorPlacements() => new(_installedDetectorPositionAndKeys);

        private void SetParticleEnergyRangeWithValidation(float min, float max)
        {
            max = Mathf.Max(min, max, 0f);
            min = Mathf.Max(Mathf.Min(min, max), 0f);

            _particleEnergyMin.Value = min;
            _particleEnergyMax.Value = max;
        }

        public void TrySetParticleEnergyMin(float particleEnergyMin)
        {
            _particleEnergyMin.Value = Mathf.Max(Mathf.Min(particleEnergyMin, _particleEnergyMax.Value), 0f);
        }
        
        public void TrySetParticleEnergyMax(float particleEnergyMax)
        {
            _particleEnergyMax.Value = Mathf.Max(particleEnergyMax, _particleEnergyMin.Value, 0f);
        }

        private void SetUsingParticleKeys(List<string> usingParticleKeys)
        {
            _usingParticleKeys.Clear();
            
            foreach(var key in usingParticleKeys)
            {
                _usingParticleKeys.Add(key);
            }
        }

        // ToDo: 現状ファイルロード時しかこのメソッド使ってないからいいけど、本来はDictionary<(int, int), string>を受け取るのが適切
        private void SetInstalledDetectorPositionAndKeys(Dictionary<string, string> table)
        {
            _installedDetectorPositionAndKeys.Clear();
            foreach (var (key, value) in table)
            {
                _installedDetectorPositionAndKeys[key.ToTuple()]= value;
            }
        }

        public void SetMagneticFieldX(float magneticFieldX)
        {
            var vec = _magneticFieldVector.Value;
            vec.x = magneticFieldX;
            _magneticFieldVector.Value = vec;
        }
        
        public void SetMagneticFieldY(float magneticFieldY)
        {
            var vec = _magneticFieldVector.Value;
            vec.y = magneticFieldY;
            _magneticFieldVector.Value = vec;
        }

        public void SetMagneticFieldZ(float magneticFieldZ)
        {
            var vec = _magneticFieldVector.Value;
            vec.z = magneticFieldZ;
            _magneticFieldVector.Value = vec;
        }

        public void SetParticleState(string particle, bool isOn)
        {
            if (isOn)
            {
                if (!_usingParticleKeys.Contains(particle))
                {
                    _usingParticleKeys.Add(particle);
                }
            }
            else
            {
                _usingParticleKeys.Remove(particle);
            }
        }

        public void LoadFile(string filePath)
        {
            var data = FileIOUtil.ReadSceneData<CanvasDataFileInfo>(filePath);

            SetCellSize(data.cellSize);
            SetCanvasSize(data.canvasSize);
            SetSimulationWorldDepth(data.simulationWorldDepth);
            SetParticleEnergyRangeWithValidation(data.particleEnergyMin, data.particleEnergyMax);
            SetMagneticFieldVector(data.magneticFieldVector.ToVector3());
            SetUsingParticleKeys(data.usingParticleKeys);
            SetInstalledDetectorPositionAndKeys(data.installedDetectorPositionAndKeys);
        }

        public void PutDetector((int, int) position, string detectorKey, bool isActiveSymmetry)
        {
            RegisterDetector(position, detectorKey);

            if (!isActiveSymmetry) return;

            var subCursorPosition = HoneycombCoordinate.MakeSubCursorPosition(position);

            foreach (var subPosition in subCursorPosition)
            {
                RegisterDetector(subPosition, detectorKey);
            }
        }

        public void RemoveDetector((int, int)position, bool isActiveSymmetry)
        {
            UnregisterDetector(position);
            
            if (!isActiveSymmetry) return;
            
            var subCursorPosition = HoneycombCoordinate.MakeSubCursorPosition(position);

            foreach (var subPosition in subCursorPosition)
            {
                UnregisterDetector(subPosition);
            }
        }

        public List<string> GetUsingParticleKeys()
        {
            return _usingParticleKeys.ToList();
        }
    }
}