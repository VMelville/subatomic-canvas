using System;
using TMPro;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class DataContentView : MonoBehaviour
    {
        [SerializeField] private Button trashButton;
        [SerializeField] private Button loadButton;

        [SerializeField] private Text titleText;
        [SerializeField] private TMP_Text dateText;

        [SerializeField] private RectTransform contentRt;

        [SerializeField] private Image backgroundImage;

        [SerializeField] private Image saveEffectOverlay;
        
        private static readonly Color ActiveColor = new(0.7f, 0.7f, 0.7f);
        private static readonly Color InactiveColor = new(0.4f, 0.4f, 0.4f);
        private static readonly Color SaveEffectColor = new(1.0f, 0.0f, 0.0f, 0.5f);

        public Button.ButtonClickedEvent OnClickTrashButton => trashButton.onClick;
        public Button.ButtonClickedEvent OnClickLoadButton => loadButton.onClick;
        public IObservable<PointerEventData> OnClickView => backgroundImage.OnPointerClickAsObservable();

        public void SetTitleText(string text) => titleText.text = text;
        public void SetDateText(string text) => dateText.text = text;
        public void SetActiveColor(bool isActive) => backgroundImage.color = isActive ? ActiveColor : InactiveColor;
        
        public void DisplayTrashButton(bool isDisplay, bool isImmediate = false)
        {
            var trashButtonTransform = (RectTransform)trashButton.transform;

            var isDisplayCoef = isDisplay ? 1f : 0f;
            
            if (isImmediate)
            {
                trashButtonTransform.sizeDelta = Vector2.one * isDisplayCoef * 16f;
                contentRt.sizeDelta = Vector2.left * isDisplayCoef * 24f;
            }
            else
            {
                trashButtonTransform.TweenSizeDelta(Vector2.one * isDisplayCoef * 16f, 0.3f);
                contentRt.TweenSizeDelta(Vector2.left * isDisplayCoef * 24f, 0.3f);
            }
        }
        
        public void PlaySaveEffect()
        {
            saveEffectOverlay.color = SaveEffectColor;
            saveEffectOverlay.TweenColor(Color.clear, 1.0f);
        }
    }
}