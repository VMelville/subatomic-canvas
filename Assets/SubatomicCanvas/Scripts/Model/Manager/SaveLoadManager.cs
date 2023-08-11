using System;
using System.IO;
using SubatomicCanvas.Utility;
using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public class SaveLoadManager : IDisposable
    {
        public IReadOnlyReactiveDictionary<string, CanvasDataFileInfo> CanvasDataFiles => _canvasDataFiles;
        public IReadOnlyReactiveProperty<string> FileNameCandidate => _fileNameCandidate;
        public IReadOnlyReactiveProperty<bool> IsDisplayTrashButton => _isDisplayTrashButton; // ToDo: これはViewで保持して良い

        private readonly ReactiveDictionary<string, CanvasDataFileInfo> _canvasDataFiles = new();
        private readonly StringReactiveProperty _fileNameCandidate = new();
        private readonly BoolReactiveProperty _isDisplayTrashButton = new();

        public void ToggleDisplayTrashButton() => _isDisplayTrashButton.Value ^= true;
        
        public void SetFileNameCandidate(string filePath)
        {
            _fileNameCandidate.Value = Path.GetFileNameWithoutExtension(filePath);
        }

        public void ReloadFiles()
        {
            _canvasDataFiles.Clear();

            var filePaths = FileIOUtil.GetFileList("*.json");

            foreach (var filePath in filePaths)
            {
                var data = FileIOUtil.ReadSceneData<CanvasDataFileInfo>(filePath);
                _canvasDataFiles[filePath] = data;
            }
        }

        public void SaveFile(CanvasManager manager)
        {
            Debug.LogWarning("ToDo: CanvasStateごと渡してるの大掛かりすぎるかな");
            
            // json データを準備
            var data = new CanvasDataFileInfo(_fileNameCandidate.Value, manager);

            var jsonData = FileIOUtil.FormatToJsonData(data);
            
            // 保存したいファイルパスを準備
            var directoryPath = Path.Combine(Application.dataPath, "SceneData");
            
            // ToDo: あくまで入力を受けてStateの値を書き換えるだけに留めるべき
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            var filePath = Path.Combine(directoryPath, _fileNameCandidate + ".json");
            
            // 書き込み
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Data saved to: " + filePath);

            // State に登録
            _canvasDataFiles[filePath] = data;
            
            Debug.LogWarning("ToDo: セーブしたときにエフェクトを追加");
            // _saveLoadView.PlaySaveEffect(filePath);
        }

        public void DeleteFile(string filePath)
        {
            FileIOUtil.DeleteJsonFile(filePath);
            ReloadFiles();
        }

        public void Dispose()
        {
            _canvasDataFiles?.Dispose();
            _fileNameCandidate?.Dispose();
            _isDisplayTrashButton?.Dispose();
        }
    }
}