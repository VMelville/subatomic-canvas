using System;
using UniRx;

namespace SubatomicCanvas.Presenter
{
    public abstract class ControllerBase : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        void IDisposable.Dispose() => _disposable.Dispose();

        public void AddDisposable(IDisposable item)
        {
            _disposable.Add(item);
        }
    }
}