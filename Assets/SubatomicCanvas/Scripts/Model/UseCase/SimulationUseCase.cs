using System;
using ParticleSim;
using ParticleSim.CSGSolid;
using ParticleSim.Volume;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SubatomicCanvas.Model.UseCase
{
    public class SimulationUseCase
    {
        private const double GeV = 1000.0;
        private const double MeV = 1.0;
        private const double KeV = 0.001;
        
        public ParticleSim.Result.SimulationResult RunSimulation(CanvasData canvasData)
        {
            var particleGun = PickupParticleGun();
            
            var worldLv = new LogicalVolume
            (
                Box.CreateBoxFromUnityCubeScale("World", new Vector3(8f, 8f, 5f)),
                "World",
                "G4_AIR",
                ThreeVector.MagFieldToG4Vec(new Vector3(0f, 0f, 0.5f))
            ).GetPointer();

            var worldPvp = new PVPlacement
            (
                RotationMatrix.RotationToG4Mat(Quaternion.identity),
                ThreeVector.PositionToG4Vec(Vector3.zero),
                "World",
                worldLv,
                IntPtr.Zero,
                0
            );

            Debug.LogWarning("ToDo: 現状シミュレーションには一切ジオメトリを反映していません。");
            // _cellsVisualizer.PvPlacements(worldPvp, 2);

            // foreach (var cell in _dataStoreManager.GetCellStatus())
            // {
            //     if (cell.cellType == 0) continue;
            //     
            //     var logicalVolume = _availableDetectorLogicalVolumeList.GetLogicalVolume(cell.cellType);
            //
            //     var rotation = cell.GetRotation();
            //     var position = cell.GetPosition();
            //
            //     _ = new PVPlacement
            //     (
            //         RotationMatrix.RotationToG4Mat(rotation),
            //         ThreeVector.PositionToG4Vec(position),
            //         logicalVolume.GetName(),
            //         logicalVolume.GetPointer(),
            //         worldPvp.GetPointer(),
            //         cell.id
            //     );
            // }

            return Simulator.RunSimulation(particleGun, worldPvp);

            // _simulationCompletePublisher.Publish(new SimulationCompleteMessage(result));
        }

        private ParticleGun PickupParticleGun()
        {
            Debug.LogWarning("ToDo: 一旦仮でKs中間子を設定しています。選択機構を実装してください");
            return new ParticleGun("kaon0S", Random.Range(100f, 300f) * MeV);
        }
    }
}