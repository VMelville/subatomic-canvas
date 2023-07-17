using System.Collections.Generic;
using SubatomicCanvas.Utility.Tween;
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

        private bool _isOpen;
        private int _nowIndex;

        private void Start()
        {
            Debug.LogWarning("ToDo: 【優先度:高】MenuViewが状態を持っているのは良くないです。MVPパターンを用いてください。");
            
            if (_isOpen)
            {
                Open(true);
            }
            else
            {
                Close(true);
            }
            
            ChangeContent(_nowIndex);
            
            menuDropdown.onValueChanged.AddListener(ChangeContent);
        
            menuButton.onClick.AddListener(Toggle);
        }

        private void ChangeContent(int index)
        {
            for (var i = 0; i < contents.Count; i++)
            {
                contents[i].SetActive(i == index);
            }

            _nowIndex = index;
        }
        
        private void Toggle()
        {
            _isOpen = !_isOpen;
        
            if (_isOpen)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
        
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
