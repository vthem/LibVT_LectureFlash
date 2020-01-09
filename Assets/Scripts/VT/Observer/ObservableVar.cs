using System;

namespace VT.Observer
{
    public class ObservableVar<T> : IDisposable, IGenericObservable<T>
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                ObserverRegistry.NotifyObserver(this);
            }
        }

        public string Name { get; }

        object IObservable.Value => Value;

        public ObservableVar(string name)
        {
            Name = name;
            VarRegistry.Add(this);
        }

        public void Dispose()
        {
            VarRegistry.Remove(this);
        }
    }
}