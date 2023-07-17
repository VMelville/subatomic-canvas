﻿using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CameraPresenter : IStartable
    {
        [Inject] private CameraState _cameraState;
        [Inject] private CameraView _cameraView;
        
        public void Start()
        {
            Debug.LogWarning("ToDo: カメラ操作");
            
            _cameraState.position.Subscribe(_cameraView.SetPosition);
            _cameraState.rotation.Subscribe(_cameraView.SetRotation);
            _cameraState.orthographic.Subscribe(_cameraView.SetOrthographic);
            _cameraState.orthographicSize.Subscribe(_cameraView.SetOrthographicSize);
        }
    }
}