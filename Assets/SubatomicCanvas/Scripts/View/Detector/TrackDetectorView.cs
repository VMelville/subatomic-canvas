using System.Collections.Generic;
using System.Linq;
using ParticleSim.Result;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public class TrackDetectorView : DetectorViewBase
    {
        [SerializeField] private GameObject dropletPrefab;
        
        private const float DropletLength = 0.005f;
        private const float MaxDropletSize = 30f;
        
        // ToDo: Viewが状態を持ってしまっている…
        private List<(float, RectTransform)> _droplets = new();
        
        // 表示されていないもののうち最も若い Index
        private int _prevDropletIndex;
        
        public override string DetectorKey => "TrackDetectorV1";
        
        public override void ClearSense()
        {
            _droplets.Clear();
            _prevDropletIndex = 0;

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public override void SetSize(float size)
        {
        }

        public override void AddSense(TrajectoryPoint point)
        {
            var stepVector = point.UnityScalePostPosition - point.UnityScalePrePosition;

            var dropletNum = (int)(stepVector.magnitude / DropletLength) + 1;

            var dropletVector = stepVector / dropletNum;

            var stepStartTime = point.PreStepPointGlobalTime;
            var stepSpanTime = point.PostStepPointGlobalTime - point.PreStepPointGlobalTime;
        
            for (var i = 0; i < dropletNum; i++)
            {
                var t = Random.value;

                var obj = Instantiate(dropletPrefab, transform);
                var rtf = (RectTransform) obj.transform;

                // ToDo: ここの transform.parent.parent 、構造に依存しているのでいまいち
                rtf.anchoredPosition = (Vector2)(point.UnityScalePrePosition + dropletVector * (i + t)) * 1000f - ((RectTransform)transform.parent.parent).anchoredPosition;
                rtf.sizeDelta = Vector2.one * Mathf.Min(Mathf.Pow(point.TotalEnergyDeposit / dropletNum, 1f / 3f), 1f) * MaxDropletSize;
                rtf.localScale = Vector3.zero;
            
                _droplets.Add((stepStartTime + stepSpanTime * (i + t) / dropletNum, rtf));
            }
        }

        public override void ReadySense()
        {
            _droplets = new List<(float, RectTransform)>(_droplets.OrderBy(x => x.Item1));
        }

        public override void SeekTime(float time)
        {
            while (true)
            {
                // 霧を表示
                if (_prevDropletIndex < _droplets.Count && time > _droplets[_prevDropletIndex].Item1)
                {
                    _droplets[_prevDropletIndex].Item2.localScale = Vector3.one;
                    _prevDropletIndex++;
                    continue;
                }
            
                // 霧を非表示
                if (_prevDropletIndex > 0 && time < _droplets[_prevDropletIndex - 1].Item1)
                {
                    _prevDropletIndex--;
                    _droplets[_prevDropletIndex].Item2.localScale = Vector3.zero;
                    continue;
                }
            
                break;
            }
        }
    }
}