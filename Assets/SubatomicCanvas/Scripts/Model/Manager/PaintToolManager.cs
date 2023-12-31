﻿using System;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class PaintToolManager : IDisposable
    {
        public IReadOnlyReactiveProperty<string> ActiveDetectorKey => _activeDetectorKey;
        public IReadOnlyReactiveProperty<bool> IsActiveSymmetry => _isActiveSymmetry;

        // initialValue は仮で設定してあります。
        private readonly StringReactiveProperty _activeDetectorKey = new("TrackDetectorV1");
        private readonly BoolReactiveProperty _isActiveSymmetry = new(true);

        public const string ViewModeKey = "ViewMode";

        public void SetActiveDetectorKey(string activeDetectorKey) => _activeDetectorKey.Value = activeDetectorKey;
        public void ToggleActiveSymmetry() => _isActiveSymmetry.Value ^= true;

        public void Dispose()
        {
            _activeDetectorKey?.Dispose();
            _isActiveSymmetry?.Dispose();
        }
    }
}