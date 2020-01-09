using System;
using System.Collections.Generic;

namespace VT.Collection
{
    public class Registry<Key, Value>
    {
        private DictionaryList<Key, Value> elements = new DictionaryList<Key, Value>();
        public event Action<Key, Value> Added;
        public event Action<Key, Value> Removed;

        public void Remove(Key k, Value v)
        {
            elements.Remove(k, v);
            Removed?.Invoke(k, v);
        }

        public void Add(Key k, Value v)
        {
            elements.Add(k, v);
            Added?.Invoke(k, v);
        }

        public bool TryGet(Key k, out List<Value> v)
        {
            return elements.TryGetList(k, out v);
        }
    }
}