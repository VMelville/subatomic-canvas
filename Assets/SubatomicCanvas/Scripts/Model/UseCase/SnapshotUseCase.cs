using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SubatomicCanvas.Model.UseCase
{
    public class SnapshotUseCase
    {
        private const string SnapshotDirectory = "SubatomicCanvas";
        
        public readonly UnityEvent onTookSnapshot = new();
        
        public async void TakeSnapshot()
        {
            await CaptureSnapshotAsync();
        }
        
        private async UniTask CaptureSnapshotAsync()
        {
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

            var latestParticle = "XXXX";
            Debug.LogWarning("ToDo: Particleの名前をファイル名に組み込む");

            var nowTime = 3.141592f;
            Debug.LogWarning("ToDo: 時刻をファイル名に組み込む");

            var snapshotFilename = "snapshot_" +
                                   DateTime.Now.ToString("yyyyMMdd_HHmmss_") +
                                   latestParticle + "_" +
                                   nowTime.ToString("F2") + "ns.png";

            var picturesFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var snapshotFolderPath = Path.Combine(picturesFolderPath, SnapshotDirectory);
            if (!Directory.Exists(snapshotFolderPath))
            {
                Directory.CreateDirectory(snapshotFolderPath);
            }

            ScreenCapture.CaptureScreenshot(Path.Combine(snapshotFolderPath, snapshotFilename));

            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

            onTookSnapshot.Invoke();
        }
    }
}