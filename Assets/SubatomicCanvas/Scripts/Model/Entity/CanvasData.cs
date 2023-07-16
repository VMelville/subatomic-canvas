using System.Collections.Generic;
using System.Numerics;

namespace SubatomicCanvas.Model
{
    public class CanvasData
    {
        public List<string> usingParticleKeys;
        public List<((int, int), string)> installedDetectorPositionAndKeys;
        public Vector3 magneticFieldVector;
        public float cellSize;
        public int canvasSize;

        public CanvasData()
        {
            usingParticleKeys = new List<string>();
            installedDetectorPositionAndKeys = new List<((int, int), string)>();
            magneticFieldVector = new Vector3();
            cellSize = 0.1f;
            canvasSize = 10;
        }
    }
}