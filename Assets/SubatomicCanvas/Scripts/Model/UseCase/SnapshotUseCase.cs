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
        
        public async void TakeSnapshot(string particleName, float timeValue)
        {
            await CaptureSnapshotAsync(particleName, timeValue);
        }
        
        private async UniTask CaptureSnapshotAsync(string particleName, float timeValue)
        {
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

            var snapshotFilename = "snapshot_" +
                                   DateTime.Now.ToString("yyyyMMdd_HHmmss_") +
                                   particleName + "_" +
                                   timeValue.ToString("F2") + "ns.png";

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