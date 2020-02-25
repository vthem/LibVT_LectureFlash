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
                ObserverSystem.Observers.NotifyObserver(this);
            }
        }

        public string Name { get; }

        object IObservable.Value => Value;

        public string TypeString => string.IsNullOrEmpty(typeString) ? typeString = typeof(T).ToString() : typeString;
        private string typeString = null;

        public ObservableVar(string name)
        {
            Name = name;
            ObserverSystem.Vars.Add(this);
        }

        public void Dispose()
        {
            ObserverSystem.Vars.Remove(this);
        }
    }
}