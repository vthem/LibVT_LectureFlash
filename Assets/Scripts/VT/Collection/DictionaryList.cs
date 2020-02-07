using System.Collections.Generic;

namespace VT.Collection
{
    public class DictionaryList<Key, Value>
    {
        private readonly Dictionary<Key, List<Value>> dic = new Dictionary<Key, List<Value>>();

        public void Add(Key key, Value value)
        {
            if (!dic.TryGetValue(key, out List<Value> values))
            {
                values = new List<Value>();
                dic.Add(key, values);
            }
            values.Add(value);
        }

        public void RemoveAll(Key key, Value value)
        {
            if (dic.TryGetValue(key, out List<Value> values))
            {
                values.RemoveAll((rv) => { return rv.Equals(value); });
                if (values.Count == 0)
                {
                    dic.Remove(key);
                }
            }
        }

        public void Remove(Key key, Value value)
        {
            if (dic.TryGetValue(key, out List<Value> values))
            {
                values.Remove(value);
                if (values.Count == 0)
                {
                    dic.Remove(key);
                }
            }
        }

        public void RemoveList(Key key)
        {
            dic.Remove(key);
        }

        public bool TryGetList(Key key, out List<Value> values)
        {
            return dic.TryGetValue(key, out values);
        }

        public void Clear()
        {
            dic.Clear();
        }
    }
}