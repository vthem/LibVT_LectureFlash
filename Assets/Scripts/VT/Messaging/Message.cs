using System.Collections.Generic;

namespace VT.Messaging
{
    public class Message
    {
        private Dictionary<string, object> data = new Dictionary<string, object>();

        public string Name { get; set; }

        public Message Set<T>(string key, T value)
        {
            data[key] = value;
            return this;
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = default(T);
            if (data.TryGetValue(key, out object tmp))
            {
                if (tmp is T)
                {
                    value = (T)tmp;
                    return true;
                }
            }
            return false;
        }

        public bool HasKey(string key)
        {
            return data.ContainsKey(key);
        }

        public bool ValueEqual<T>(string key, T left)
        {
            return data.TryGetValue(key, out object right) && left.Equals(right);
        }

        public override string ToString()
        {
            string toString = $"{Name} ";
            foreach (var kv in data)
            {
                toString += $" {kv.Key}={kv.Value}";
            }
            return toString;
        }
    }
}