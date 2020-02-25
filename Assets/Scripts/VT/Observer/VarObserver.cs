using System;

namespace VT.Observer
{
    public class VarObserver : IObserver, IDisposable
    {
        private readonly Action<object> callback;

        public string Name { get; }

        public string VarName { get; }

        public VarObserver(string observerName, string varName, Action<object> callback)
            : this(observerName, varName, callback, true) { }

        public VarObserver(string observerName, string varName, Action<object> callback, bool notifyOnCreate)
        {
            Name = observerName;
            VarName = varName;
            this.callback = callback;
            ObserverSystem.Observers.Add(this, notifyOnCreate);
        }

        public void Dispose()
        {
            ObserverSystem.Observers.Remove(this);
        }

        public void VarUpdatedHandler(IObservable observable)
        {
            callback.Invoke(observable.Value);
        }
    }
}