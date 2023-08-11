using System;

namespace SubatomicCanvas.Presenter
{
    public static class DisposableExtensions
    {
        public static T AddTo<T>(this T disposable, ControllerBase controller)
            where T: IDisposable
        {
            controller.AddDisposable(disposable);
            return disposable;
        } 
    }
}