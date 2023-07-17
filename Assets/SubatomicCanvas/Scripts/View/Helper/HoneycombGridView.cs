using System.Linq;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public class HoneycombGridView : MonoBehaviour
    {
        [SerializeField] private GridUnitView gridUnitPrefab;

        public void ClearGrid()
        {
            var children = (from Transform child in transform select child.gameObject).ToList();
            children.ForEach(Destroy);
        }
        
        public void DrawGrid(int canvasSize)
        {
            for (var i = -canvasSize; i < canvasSize; i++)
            {
                for (var j = Mathf.Max(i, 0) - canvasSize; j < Mathf.Min(i, 0) + canvasSize; j++)
                {
                    var isActive = (j < canvasSize - 1, i > -canvasSize, i - j < canvasSize);
                    var position = (j, i);
                    
                    var view = Instantiate(gridUnitPrefab, transform);
                    view.SetActive(isActive);
                    view.SetPosition(position);
                }
            }
        }
    }
}