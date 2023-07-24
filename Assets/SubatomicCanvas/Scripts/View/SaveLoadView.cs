using System.Collections.Generic;
using SubatomicCanvas.Model;
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
        private List<DataContentView> _dataContents = new();

        public UnityEvent<string> onClickLoadFileButton;
        public UnityEvent<string> onClickTrashFileButton;
        
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
            
            view.SetTitleText(info.title);
            view.SetDateText(info.saveDate);
            view.SetActiveColor(isActive);
            view.DisplayTrashButton(isDisplayTrashButton, true);

            if (isActive)
            {
                view.PlaySaveEffect();
            }

            _dataContents.Add(view);
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