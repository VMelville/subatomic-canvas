using System.Collections.Generic;
using System.Linq;
using ParticleSim.Result;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public class LineVisualizeView : MonoBehaviour
    {
        [SerializeField] private LineRenderer linePrefab;
        
        public void ClearLine()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void DrawLine(List<Trajectory> trajectories)
        {
            foreach (var trajectory in trajectories)
            {
                var lineInstance = Instantiate(linePrefab, transform);

                var line = lineInstance.GetComponent<LineRenderer>();

                var linePositions = 
                    (from trajectoryPoint in trajectory.Points
                        select trajectoryPoint.UnityScalePostPosition).ToArray();

                var lineColor = GetLineColor(trajectory.Charge);

                line.positionCount = linePositions.Length;
                line.SetPositions(linePositions);
                line.endColor = line.startColor = lineColor;
            }
        }
        
        private static Color GetLineColor(float charge)
        {
            if (charge > 0)
            {
                return new Color(0.5f, 0.5f, 1f, 0.4f);
            }
            
            if (charge < 0)
            {
                return new Color(1f, 0.5f, 0.5f, 0.4f);
            }
            
            return new Color(0.5f, 1f, 0.5f, 0.4f);
        }
    }
}