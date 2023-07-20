using System.Collections.Generic;
using System.Linq;
using ParticleSim.Result;
using TMPro;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public class CalorimeterView : DetectorViewBase
    {
        [SerializeField] private TMP_Text calorieText;
        [SerializeField] private RectTransform meterRectTransform;
        
        // ToDo: Viewが状態を持ってしまっている…
        private List<(float, float, float)> _calorieKeyframes = new();
        private int _calorieKeyframeIndex = -1;
        
        public override string DetectorKey => "CalorimeterV1";
        
        public override void ClearSense()
        {
            _calorieKeyframes.Clear();
            _calorieKeyframes.Add((0f, 0f, 0f));
            _calorieKeyframeIndex = 0;
            UpdateVisualizer();
        }

        public override void AddSense(TrajectoryPoint point)
        {
            _calorieKeyframes.Add((point.PreStepPointGlobalTime, point.TotalEnergyDeposit, point.PostStepPointGlobalTime - point.PreStepPointGlobalTime));
        }

        public override void ReadySense()
        {
            _calorieKeyframes = new List<(float, float, float)>(_calorieKeyframes.OrderBy(x => x.Item1));
        
            for (var i = 1; i < _calorieKeyframes.Count; i++)
            {
                _calorieKeyframes[i] = (
                    _calorieKeyframes[i].Item1,
                    _calorieKeyframes[i - 1].Item2 + _calorieKeyframes[i].Item2, 
                    _calorieKeyframes[i].Item3
                );
            }
        }


        public override void SeekTime(float time)
        {
            while (true)
            {
                if (_calorieKeyframeIndex + 1 < _calorieKeyframes.Count && time > _calorieKeyframes[_calorieKeyframeIndex + 1].Item1)
                {
                    UpdateVisualizer();
                    _calorieKeyframeIndex++;
                    continue;
                }

                if (_calorieKeyframeIndex >= 0 && time < _calorieKeyframes[_calorieKeyframeIndex].Item1)
                {
                    _calorieKeyframeIndex--;
                    UpdateVisualizer();
                    continue;
                }
            
                break;
            }
        }
        
        private void UpdateVisualizer()
        {
            if (_calorieKeyframeIndex == -1)
            {
                meterRectTransform.sizeDelta = Vector2.zero;
                calorieText.text = "";
            
            }
        
            var (_, energy, duration) = _calorieKeyframes[_calorieKeyframeIndex];
            meterRectTransform.sizeDelta = Vector2.one * Mathf.Pow(energy, 1f / 3f) * 10f;
            calorieText.text = energy > 0 ? energy.ToString("F1") + " MeV" : "";
        }
    }
}