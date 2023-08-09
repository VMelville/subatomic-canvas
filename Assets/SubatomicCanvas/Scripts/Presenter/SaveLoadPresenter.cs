﻿using System.IO;
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
            _saveLoadState.canvasDataFiles.ObserveReplace().Subscribe(OnReplaceCanvasDataFiles);
            _saveLoadState.canvasDataFiles.ObserveReset().Subscribe(OnResetCanvasDataFiles);
            _saveLoadState.isDisplayTrashButton.Subscribe(DisplayTrashButton);
            _saveLoadState.fileNameCandidate.Subscribe(OnChangeFileNameCandidate);
            
            _saveLoadView.OnClickDisplayTrashButton.AddListener(OnClickDisplayTrashButton);
            _saveLoadView.OnClickReloadButton.AddListener(ReloadFiles);
            _saveLoadView.OnClickSaveButton.AddListener(OnClickSaveButton);
            _saveLoadView.OnChangeFileName.AddListener(OnChangeFileName);
            _saveLoadView.OnClickLoadFileButton.AddListener(OnClickLoadFileButton);
            _saveLoadView.OnClickTrashFileButton.AddListener(OnClickTrashFileButton);
            _saveLoadView.OnClickView.AddListener(OnClickView);
            
            ReloadFiles();
        }
        
        // --------------------------
        // Model -> Presenter -> View
        // --------------------------
        private void OnAddCanvasDataFiles(DictionaryAddEvent<string, CanvasDataFileInfo> addEvent)
        {
            var filePath = addEvent.Key;
            var fileInfo = addEvent.Value;
            var isActive = fileInfo.title == _saveLoadState.fileNameCandidate.Value;
            var isDisplayTrashButton = _saveLoadState.isDisplayTrashButton.Value;
            _saveLoadView.AddDataContent(filePath, fileInfo, isActive, isDisplayTrashButton);
        }
        
        private void OnReplaceCanvasDataFiles(DictionaryReplaceEvent<string, CanvasDataFileInfo> replaceEvent)
        {
            var filePath = replaceEvent.Key;
            var fileInfo = replaceEvent.NewValue;
            _saveLoadView.ReplaceDataContent(filePath, fileInfo);
        }

        private void OnResetCanvasDataFiles(Unit _) => _saveLoadView.ClearDataContent();
        
        private void DisplayTrashButton(bool isDisplay) => _saveLoadView.DisplayTrashButton(isDisplay);
        
        private void OnChangeFileNameCandidate(string fileName)
        {
            _saveLoadView.SetSaveButtonInteractable(FileIOUtil.CouldSaveTitle(fileName));
            var directoryPath = Path.Combine(Application.dataPath, "SceneData");
            var filePath = Path.Combine(directoryPath, fileName + ".json");
            _saveLoadView.ChangeActiveFilePath(filePath);
        }
        
        // --------------------------
        // View -> Presenter -> Model
        // --------------------------
        
        private void OnClickDisplayTrashButton() => _saveLoadState.isDisplayTrashButton.Value ^= true;
        
        private void ReloadFiles()
        {
            _saveLoadState.canvasDataFiles.Clear();

            var filePaths = FileIOUtil.GetFileList("*.json");

            foreach (var filePath in filePaths)
            {
                var data = FileIOUtil.ReadSceneData<CanvasDataFileInfo>(filePath);
                _saveLoadState.canvasDataFiles[filePath] = data;
            }
        }
        
        private void OnClickSaveButton()
        {
            // json データを準備
            var data = new CanvasDataFileInfo(_saveLoadState.fileNameCandidate.Value, _canvasState);

            var jsonData = FileIOUtil.FormatToJsonData(data);
            
            // 保存したいファイルパスを準備
            var directoryPath = Path.Combine(Application.dataPath, "SceneData");
            
            // ToDo: あくまで入力を受けてStateの値を書き換えるだけに留めるべき
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            var filePath = Path.Combine(directoryPath, _saveLoadState.fileNameCandidate + ".json");
            
            // 書き込み
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Data saved to: " + filePath);

            // State に登録
            _saveLoadState.canvasDataFiles[filePath] = data;
            _saveLoadView.PlaySaveEffect(filePath);
        }

        private void OnChangeFileName(string fileName)
        {
            _saveLoadState.fileNameCandidate.Value = fileName;
        }
        
        private void OnClickLoadFileButton(string filePath)
        {
            var data = FileIOUtil.ReadSceneData<CanvasDataFileInfo>(filePath);
            
            _canvasState.cellSize.Value = data.cellSize;
            _canvasState.canvasSize.Value = data.canvasSize;
            _canvasState.simulationWorldDepth.Value = data.simulationWorldDepth;
            _canvasState.particleEnergyMin.Value = data.particleEnergyMin;
            _canvasState.particleEnergyMax.Value = data.particleEnergyMax;
            _canvasState.magneticFieldVector.Value = data.magneticFieldVector.ToVector3();
            
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
        }
        
        private void OnClickTrashFileButton(string filePath)
        {
            FileIOUtil.DeleteJsonFile(filePath);
            ReloadFiles();
        }

        private void OnClickView(string path)
        {
            _saveLoadState.fileNameCandidate.Value = Path.GetFileNameWithoutExtension(path);
        }
    }
}