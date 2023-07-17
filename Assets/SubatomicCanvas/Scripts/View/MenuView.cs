using System.Collections.Generic;
using SubatomicCanvas.Utility.Tween;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private GameObject menuContent;
        [SerializeField] private TMP_Dropdown menuDropdown;
        [SerializeField] private List<GameObject> contents;

        public UnityEvent onClickMenuButton => menuButton.onClick;
        public UnityEvent<int> onChangePage => menuDropdown.onValueChanged;
        
        public void SetOpenCloseStateImmediately(bool isOpen)
        {
            if (isOpen)
            {
                Open(true);
            }
            else
            {
                Close(true);
            }
        }

        public void SetOpenCloseState(bool isOpen)
        {
            if (isOpen)
            {
                Open();
            }
            else
            {
                Close();
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
        private void Open(bool isImmediate = false)
        {
            var rt = (RectTransform)transform;
            rt.anchorMin = Vector2.right;
            rt.anchorMax = Vector2.one;

            if (isImmediate)
            {
                rt.sizeDelta = new Vector2(240f, -124f);
            }
            else
            {
                rt.sizeDelta = new Vector2(32f, 32f - Screen.height);
                rt.TweenSizeDelta(new Vector2(240f, -124f), 0.3f, this);
            }
        
            menuContent.SetActive(true);
        }

        // ToDo: ここ、マジックナンバーになってます
        private void Close(bool isImmediate = false)
        {
            var rt = (RectTransform)transform;
            rt.anchorMin = Vector2.one;
            rt.anchorMax = Vector2.one;

            if (isImmediate)
            {
                rt.sizeDelta = new Vector2(32f, 32f);
            }
            else
            {
                rt.sizeDelta = new Vector2(240f, Screen.height - 124f);
                rt.TweenSizeDelta(new Vector2(32f, 32f), 0.3f, this);
            }
            
            menuContent.SetActive(false);
        }
    }
}
