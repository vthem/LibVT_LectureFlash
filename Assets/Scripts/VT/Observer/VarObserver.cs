using System;

namespace VT.Observer
{
    public class VarObserver : IObserver, IDisposable
    {
        private readonly Action<object> callback;

        public string Name { get; }

        public string VarName { get; }

        public VarObserver(string observerName, string varName, Action<object> callback)
        {
            Name = observerName;
            VarName = varName;
            this.callback = callback;
            ObserverRegistry.Add(this);
        }

        public void Dispose()
        {
            ObserverRegistry.Remove(this);
        }

        public void VarUpdatedHandler(IObservable observable)
        {
            callback.Invoke(observable.Value);
        }
    }
}