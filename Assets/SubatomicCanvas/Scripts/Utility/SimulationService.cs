﻿using System;
using System.Collections.Generic;
using ParticleSim;
using ParticleSim.CSGSolid;
using ParticleSim.Result;
using ParticleSim.Volume;
using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Utility
{
    public class SimulationService
    {
        private const string WorldName = "World";
        private const int WorldCopyNo = 0;

        public (SimulationResult, Dictionary<string, (int, int)>) RunSimulation
        (
            ReactiveDictionary<(int, int), string> installedDetectors,
            Dictionary<string, LogicalVolume> logicalVolumes,
            ParticleGun particleGun,
            Vector3 worldScale,
            Vector3 magneticField
        )
        {
            var worldLv = new LogicalVolume
            (
                Box.CreateBoxFromUnityCubeScale("World", worldScale),
                WorldName,
                "G4_AIR",
                ThreeVector.MagFieldToG4Vec(magneticField)
            ).GetPointer();

            var worldPvp = new PVPlacement
            (
                RotationMatrix.RotationToG4Mat(Quaternion.identity),
                ThreeVector.PositionToG4Vec(Vector3.zero),
                WorldName,
                worldLv,
                IntPtr.Zero,
                WorldCopyNo
            );

            var copyNo = WorldCopyNo + 1;
            var pathPositionTable = new Dictionary<string, (int, int)>();

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

                var path = WorldName + ":" + WorldCopyNo + "/" + logicalVolume.GetName() + ":" + copyNo;
                pathPositionTable[path] = position;
                
                copyNo++;
            }

            return (Simulator.RunSimulation(particleGun, worldPvp), pathPositionTable);
        }
    }
}