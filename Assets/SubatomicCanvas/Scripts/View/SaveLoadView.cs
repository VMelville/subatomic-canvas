using System.Collections.Generic;
using System.IO;
using SubatomicCanvas.Model;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SaveLoadView : MonoBehaviour
    {
        // UI
        [SerializeField] private Button displayTrashButton;
        [SerializeField] private Button reloadButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private InputField dataNameInputField;
        [SerializeField] private Transform dataShelf;
        
        // Prefab
        [SerializeField] private DataContentView dataContentPrefab;
        
        // Member
        private Dictionary<string, DataContentView> _dataContents = new();

        public UnityEvent<string> onClickLoadFileButton;
        public UnityEvent<string> onClickTrashFileButton;
        public UnityEvent<string> onClickView;
        
        public UnityEvent onClickDisplayTrashButton => displayTrashButton.onClick;
        public UnityEvent onClickReloadButton => reloadButton.onClick;
        public UnityEvent onClickSaveButton => saveButton.onClick;
        public UnityEvent<string> onChangeFileName => dataNameInputField.onValueChanged;

        public void SetSaveButtonInteractable(bool interactable) => saveButton.interactable = interactable;
        
        public void AddDataContent(string filePath, CanvasDataFileInfo info, bool isActive, bool isDisplayTrashButton)
        {
            var view = Instantiate(dataContentPrefab, dataShelf);

            view.onClickTrashButton.AddListener(() => onClickTrashFileButton.Invoke(filePath));
            view.onClickLoadButton.AddListener(() => onClickLoadFileButton.Invoke(filePath));
            view.onClickView.Subscribe(_ => onClickView.Invoke(filePath));
            
            view.SetTitleText(info.title);
            view.SetDateText(info.saveDate);
            view.SetActiveColor(isActive);
            view.DisplayTrashButton(isDisplayTrashButton, true);

            _dataContents[filePath] = view;
        }

        public void ReplaceDataContent(string filePath, CanvasDataFileInfo info)
        {
            var view = _dataContents[filePath];

            view.SetDateText(info.saveDate);
        }

        public void PlaySaveEffect(string filePath)
        {
            _dataContents[filePath].PlaySaveEffect();
        }
        
        public void ChangeActiveFilePath(string filePath)
        {
            dataNameInputField.text = Path.GetFileNameWithoutExtension(filePath);
            
            foreach (var (path, view) in _dataContents)
            {
                view.SetActiveColor(path == filePath);
            }
        }

        public void DisplayTrashButton(bool isDisplay)
        {
            foreach (var view in _dataContents.Values)
            {
                view.DisplayTrashButton(isDisplay);
            }
        }

        public void ClearDataContent()
        {
            var childCount = dataShelf.childCount;
            
            for (var i = childCount - 1; i >= 0; i--)
            {
                Destroy(dataShelf.GetChild(i).gameObject);
            }
        }
    }
}