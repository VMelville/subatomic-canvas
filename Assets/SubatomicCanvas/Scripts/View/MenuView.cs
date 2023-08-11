using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private GameObject menuContent;
        [SerializeField] private TMP_Dropdown menuDropdown;
        [SerializeField] private List<GameObject> contents;

        public Button.ButtonClickedEvent OnClickMenuButton => menuButton.onClick;
        public TMP_Dropdown.DropdownEvent OnChangePage => menuDropdown.onValueChanged;
        
        public void SetOpenCloseState(bool isOpen, float duration)
        {
            if (isOpen)
            {
                Open(duration);
            }
            else
            {
                Close(duration);
            }
        }

        public void SetPageIndex(int pageIndex)
        {
            for (var i = 0; i < contents.Count; i++)
            {
                contents[i].SetActive(i == pageIndex);
            }
        }
        
        // ToDo: ここ、マジックナンバーになってます
        private void Open(float easingDuration)
        {
            var rt = (RectTransform)transform;
            rt.anchorMin = Vector2.right;
            rt.anchorMax = Vector2.one;

            if (easingDuration == 0.0f)
            {
                rt.sizeDelta = new Vector2(240f, -124f);
            }
            else
            {
                rt.sizeDelta = new Vector2(32f, 32f - Screen.height);
                rt.TweenSizeDelta(new Vector2(240f, -124f), easingDuration);
            }
        
            menuContent.SetActive(true);
        }

        // ToDo: ここ、マジックナンバーになってます
        private void Close(float easingDuration)
        {
            var rt = (RectTransform)transform;
            rt.anchorMin = Vector2.one;
            rt.anchorMax = Vector2.one;

            if (easingDuration == 0.0f)
            {
                rt.sizeDelta = new Vector2(32f, 32f);
            }
            else
            {
                rt.sizeDelta = new Vector2(240f, Screen.height - 124f);
                rt.TweenSizeDelta(new Vector2(32f, 32f), easingDuration);
            }
            
            menuContent.SetActive(false);
        }
    }
}
