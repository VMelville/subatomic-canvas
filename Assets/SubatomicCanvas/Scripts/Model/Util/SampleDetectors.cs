using System.Collections.Generic;
using ParticleSim.CSGSolid;
using ParticleSim.Volume;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public static class SampleDetectors
    {
        public static Dictionary<string, LogicalVolume> GetDetectorLogicalVolumeTable(float cellSize, float worldDepth)
        {
            var diameter = 0.8f * cellSize; // memo: 六角形に内接するには、0.866、つまり √3 / 2 をかけるとぴったりですが、それより若干内側にしています。なんとなくそうしているだけで、ピッタリでも問題ありません。
            var standardTubsSize = new Vector3(diameter, worldDepth * 0.5f, diameter); // Yが円柱の高さの半分の長さ。XとZは直径。単位は m
            var table = new Dictionary<string, LogicalVolume>
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
            return table;
        }
    }
}