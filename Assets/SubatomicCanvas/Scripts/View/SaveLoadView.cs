using System.Collections.Generic;
using System.IO;
using SubatomicCanvas.Model;
using SubatomicCanvas.Utility;
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

        public UnityEvent<string> OnClickLoadFileButton;
        public UnityEvent<string> OnClickTrashFileButton;
        public UnityEvent<string> OnClickView;
        
        public Button.ButtonClickedEvent OnClickDisplayTrashButton => displayTrashButton.onClick;
        public Button.ButtonClickedEvent OnClickReloadButton => reloadButton.onClick;
        public Button.ButtonClickedEvent OnClickSaveButton => saveButton.onClick;
        public InputField.OnChangeEvent OnChangeFileName => dataNameInputField.onValueChanged;

        public void AddDataContent(string filePath, CanvasDataFileInfo info, bool isActive, bool isDisplayTrashButton)
        {
            var view = Instantiate(dataContentPrefab, dataShelf);

            view.OnClickTrashButton.AddListener(() => OnClickTrashFileButton.Invoke(filePath));
            view.OnClickLoadButton.AddListener(() => OnClickLoadFileButton.Invoke(filePath));
            view.OnClickView.Subscribe(_ => OnClickView.Invoke(filePath));
            
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

        public void RemoveDataContent(string filePath, CanvasDataFileInfo info)
        {
            _dataContents.Remove(filePath);
        }

        public void PlaySaveEffect(string filePath)
        {
            _dataContents[filePath].PlaySaveEffect();
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
            
            _dataContents.Clear();
        }

        public void ChangeFileNameCandidate(string fileName)
        {
            saveButton.interactable = FileIOUtil.CouldSaveTitle(fileName);
            
            var directoryPath = Path.Combine(Application.dataPath, "SceneData");
            var filePath = Path.Combine(directoryPath, fileName + ".json");
            
            dataNameInputField.text = Path.GetFileNameWithoutExtension(filePath);
            
            foreach (var (path, view) in _dataContents)
            {
                view.SetActiveColor(path == filePath);
            }
        }
    }
}