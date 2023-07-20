using System;
using System.Collections.Generic;
using ParticleSim;
using ParticleSim.CSGSolid;
using ParticleSim.Volume;
using SubatomicCanvas.Utility;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SubatomicCanvas.Model.UseCase
{
    public class SimulationUseCase
    {
        private const double GeV = 1000.0;
        private const double MeV = 1.0;
        private const double KeV = 0.001;
        
        public ParticleSim.Result.SimulationResult RunSimulation(ReactiveDictionary<(int, int), string> installedDetectors, Dictionary<string, LogicalVolume> logicalVolumes)
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

            var copyNo = 1;

            foreach (var detector in installedDetectors)
            {
                var (position, detectorKey) = detector;
                var logicalVolume = logicalVolumes[detectorKey];
                
                _ = new PVPlacement
                (
                    RotationMatrix.RotationToG4Mat(Quaternion.identity),
                    ThreeVector.PositionToG4Vec(HoneycombCoordinate.GetPosition(position)), 
                    logicalVolume.GetName(),
                    logicalVolume.GetPointer(),
                    worldPvp.GetPointer(),
                    copyNo
                );

                copyNo++;
            }
            
            return Simulator.RunSimulation(particleGun, worldPvp);
        }

        private ParticleGun PickupParticleGun()
        {
            Debug.LogWarning("ToDo: 一旦仮でKs中間子を設定しています。選択機構を実装してください");
            return new ParticleGun("kaon0S", Random.Range(100f, 300f) * MeV);
        }
    }
}