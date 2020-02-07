using System;

namespace VT.Observer
{
    public class GenericVarObserver<T> : IGenericObserver<T>, IDisposable
    {
        private readonly Action<T> callback;

        public string VarName { get; }

        public string Name { get; }

        public GenericVarObserver(string observerName, string varName, Action<T> callback)
        {
            Name = observerName;
            VarName = varName;
            this.callback = callback;
            ObserverSystem.Observers.Add(this);
        }

        public void Dispose()
        {
            ObserverSystem.Observers.Remove(this);
        }

        public void VarUpdatedHandler(IGenericObservable<T> observable)
        {
            callback.Invoke(observable.Value);
        }

        public void VarUpdatedHandler(IObservable observable)
        {
            VarUpdatedHandler(observable as IGenericObservable<T>);
        }

        public static implicit operator GenericVarObserver<T>(VarObserver v)
        {
            throw new NotImplementedException();
        }
    }
}