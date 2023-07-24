using System.IO;
using SubatomicCanvas.Model;
using SubatomicCanvas.Utility;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SaveLoadPresenter : IStartable
    {
        // Model
        [Inject] private SaveLoadState _saveLoadState;
        [Inject] private CanvasState _canvasState;
        
        // View
        [Inject] private SaveLoadView _saveLoadView;
        
        public void Start()
        {
            _saveLoadState.canvasDataFiles.ObserveAdd().Subscribe(OnAddCanvasDataFiles);
            _saveLoadState.canvasDataFiles.ObserveReset().Subscribe(OnResetCanvasDataFiles);
            
            _saveLoadView.onClickDisplayTrashButton.AddListener(OnClickDisplayTrashButton);
            _saveLoadView.onClickReloadButton.AddListener(ReloadFiles);
            _saveLoadView.onClickSaveButton.AddListener(OnClickSaveButton);
            _saveLoadView.onChangeFileName.AddListener(OnChangeFileName);
            _saveLoadView.onClickLoadFileButton.AddListener(OnClickLoadFileButton);
            _saveLoadView.onClickTrashFileButton.AddListener(OnClickTrashFileButton);
            
            ReloadFiles();
        }

        private void OnAddCanvasDataFiles(CollectionAddEvent<(string, CanvasDataFileInfo)> addEvent)
        {
            var (filePath, fileInfo) = addEvent.Value;
            var isActive = fileInfo.title == _saveLoadState.fileNameCandidate.Value;
            var isDisplayTrashButton = _saveLoadState.isDisplayTrashButton.Value;
            _saveLoadView.AddDataContent(filePath, fileInfo, isActive, isDisplayTrashButton);
        }

        private void OnResetCanvasDataFiles(Unit _) => _saveLoadView.ClearDataContent();
        
        private void OnClickDisplayTrashButton() => _saveLoadState.isDisplayTrashButton.Value ^= true;
        
        private void ReloadFiles()
        {
            _saveLoadState.canvasDataFiles.Clear();

            var filePaths = FileIOUtil.GetFileList("*.json");

            foreach (var filePath in filePaths)
            {
                var data = FileIOUtil.ReadSceneData<CanvasDataFileInfo>(filePath);
                _saveLoadState.canvasDataFiles.Add((filePath, data));
            }
        }
        
        private void OnClickSaveButton()
        {
            // json データを準備
            var data = new CanvasDataFileInfo(_saveLoadState.fileNameCandidate.Value, _canvasState);

            var jsonData = FileIOUtil.FormatToJsonData(data);
            
            // 保存したいファイルパスを準備
            var directoryPath = Path.Combine(Application.dataPath, "SceneData");
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            var filePath = Path.Combine(directoryPath, _saveLoadState.fileNameCandidate + ".json");
            
            // 書き込み
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Data saved to: " + filePath);

            // State に登録
            _saveLoadState.canvasDataFiles.Add((filePath, data));
        }

        private void OnChangeFileName(string fileName)
        {
            _saveLoadState.fileNameCandidate.Value = fileName;
            _saveLoadView.SetSaveButtonInteractable(FileIOUtil.CouldSaveTitle(fileName));
        }

        private void OnClickLoadFileButton(string filePath)
        {
            var data = FileIOUtil.ReadSceneData<CanvasDataFileInfo>(filePath);
            
            _canvasState.usingParticleKeys.Clear();
            foreach(var key in data.usingParticleKeys)
            {
                _canvasState.usingParticleKeys.Add(key);
            }
            
            _canvasState.installedDetectorPositionAndKeys.Clear();
            foreach (var (key, value) in data.installedDetectorPositionAndKeys)
            {
                _canvasState.installedDetectorPositionAndKeys[key.ToTuple()]= value;
            }

            _canvasState.magneticFieldVector.Value = data.magneticFieldVector;
            _canvasState.cellSize.Value = data.cellSize;
            _canvasState.canvasSize.Value = data.canvasSize;
        }
        
        private void OnClickTrashFileButton(string filePath)
        {
            FileIOUtil.DeleteJsonFile(filePath);
            ReloadFiles();
        }
    }
}