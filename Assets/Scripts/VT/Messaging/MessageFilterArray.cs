using System.Collections.Generic;

namespace VT.Messaging
{
    public class MessageDataHasKey : IMessageFilter
    {
        private string key;

        public MessageDataHasKey(string key)
        {
            this.key = key;
        }

        public bool Accept(Message msg)
        {
            return msg.HasKey(key);
        }
    }

    public class MessageDataValueEqual<T> : IMessageFilter
    {
        private string key;
        private T value;

        public MessageDataValueEqual(string key, T value)
        {
            this.key = key;
            this.value = value;
        }

        public bool Accept(Message msg)
        {
            return msg.ValueEqual(key, value);
        }
    }

    public class MessageFilter : IMessageFilter
    {
        private MessageFilterArray filterArray = new MessageFilterArray();

        public bool Accept(Message msg)
        {
            return filterArray.Accept(msg);
        }

        public MessageFilter HasKey(string key)
        {
            filterArray.AddFilter(new MessageDataHasKey(key));
            return this;
        }

        public MessageFilter HasValueEqual<T>(string key, T value)
        {
            filterArray.AddFilter(new MessageDataValueEqual<T>(key, value));
            return this;
        }
    }

    public class MessageFilterArray : IMessageFilter
    {
        private readonly List<IMessageFilter> filters = new List<IMessageFilter>();

        public void AddFilter(IMessageFilter filter)
        {
            filters.Add(filter);
        }

        public bool Accept(Message msg)
        {
            for (int i = 0; i < filters.Count; ++i)
            {
                if (!filters[i].Accept(msg))
                {
                    return false;
                }
            }
            return true;
        }
    }
}