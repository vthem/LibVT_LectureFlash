using System;
using System.Collections;
using System.Collections.Generic;

namespace VT.Observer
{
    public class ObservableVarList<T> : IDisposable, IGenericObservable<List<T>>, IEnumerable<T>
    {
        public List<T> Value { get; private set; } = new List<T>();

        public string Name { get; }

        object IObservable.Value => Value;

        public int Count => Value.Count;

        public bool IsReadOnly => false;

        public T this[int index] { get => Value[index]; set => Value[index] = value; }

        private ObservableVar<T[]> added;
        private ObservableVar<T[]> removed;

        public string TypeString => string.IsNullOrEmpty(typeString) ? typeString = $"ArrayOf'{typeof(T).ToString()}'" : typeString;
        private string typeString = null;

        public ObservableVarList(string name)
        {
            Name = name;
            added = new ObservableVar<T[]>($"{name}.Added");
            removed = new ObservableVar<T[]>($"{name}.Removed");
            ObserverSystem.Vars.Add(this);
        }

        public void AddRange(List<T> items)
        {
            T[] tmp = items.ToArray();
            Value.AddRange(items);
            ObserverSystem.Observers.NotifyObserver(this);
            added.Value = tmp;
        }

        public void Dispose()
        {
            ObserverSystem.Vars.Remove(this);
            added.Dispose();
            removed.Dispose();
        }

        public int IndexOf(T item)
        {
            return Value.IndexOf(item);
        }

        public void Clear()
        {
            T[] tmp = new T[Value.Count];
            Value.CopyTo(tmp, 0);
            Value.Clear();
            ObserverSystem.Observers.NotifyObserver(this);
            removed.Value = tmp;
        }

        public bool Contains(T item)
        {
            return Value.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public void Add(T item)
        {
            T[] tmp = new T[1];
            tmp[0] = item;
            Value.Add(item);
            ObserverSystem.Observers.NotifyObserver(this);
            added.Value = tmp;
        }

        public bool Remove(T item)
        {
            bool b = Value.Remove(item);
            if (b)
            {
                T[] tmp = new T[1];
                tmp[0] = item;
                ObserverSystem.Observers.NotifyObserver(this);
                removed.Value = tmp;
            }
            return b;
        }
    }
}