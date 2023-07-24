﻿using System.Collections.Generic;
using System.Linq;
using ParticleSim;
using ParticleSim.CSGSolid;
using ParticleSim.Volume;
using SubatomicCanvas.Model;
using SubatomicCanvas.Utility;
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
        // [Inject] private AvailableParticles _availableParticles;
        [Inject] private CanvasState _canvasState;
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private ModeState _modeState;
        [Inject] private TimeState _timeState;
        
        // Model - Service
        [Inject] private SimulationService _simulationService;
        
        // View
        [Inject] private SimulatorView _simulatorView;
        
        public void Start()
        {
            _simulatorView.onClick.AddListener(OnClickRunButton);
        }

        private void OnClickRunButton()
        {
            // Detector
            // 一旦仮にここで定義
            Debug.LogWarning("ToDo: 一旦LogicalVolumeの定義をOnClickRunButton内でやっています。専用のクラスから引っ張ってくるようにしてください。");
            var standardTubsSize = new Vector3(0.08f, 3.0f, 0.08f);
            var test = new Dictionary<string, LogicalVolume>
            {
                ["TrackDetectorV1"] = new(
                    new Tubs("TrackDetectorV1", standardTubsSize),
                    "TrackDetectorV1",
                    "G4_ETHANE"
                ),
                ["CalorimeterV1"] = new(
                    new Tubs("CalorimeterV1", standardTubsSize),
                    "CalorimeterV1",
                    "G4_CESIUM_IODIDE"
                ),
                ["AbsorberV1"] = new(
                    new Tubs("AbsorberV1", standardTubsSize),
                    "AbsorberV1",
                    "G4_Fe"
                )
            };

            // Particle
            var particleDict = _canvasState.usingParticleKeys;
            var particleCount = particleDict.Count;
            if (particleCount == 0)
            {
                _simulatorView.SetText("Please select at least one particle.");
                return;
            }
            
            var randomParticleKey = particleDict.ElementAt(Random.Range(0, particleCount));
            var particleGun = new ParticleGun(randomParticleKey, Random.Range(100f, 300f)); // 単位はMeV
            _simulatorView.SetText(randomParticleKey);
            
            // シミュレーション実行
            var (result, positionPathDict) = _simulationService.RunSimulation(_canvasState.installedDetectorPositionAndKeys, test, particleGun);

            // 結果を記録
            _lastSimulationCondition.result.Value = (result, new Dictionary<string, (int, int)>(positionPathDict));
            _lastSimulationCondition.particleKey.Value = randomParticleKey;
            
            // ToDo: ここ、もうちょっとシンプルなコピー方法ありませんか？
            _lastSimulationCondition.detectorKeyDict.Clear();
            foreach (var (position, detector) in _canvasState.installedDetectorPositionAndKeys)
            {
                _lastSimulationCondition.detectorKeyDict[position] = detector;
            }

            // 時刻をリセット
            _timeState.time.Value = 0.0f;
        }
    }
}