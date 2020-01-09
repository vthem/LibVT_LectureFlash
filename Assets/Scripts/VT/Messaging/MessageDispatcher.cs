using System.Collections.Generic;

namespace VT.Messaging
{
    public class MessageDispatcher
    {
        protected Dictionary<string, List<MessageListener>> listeners = new Dictionary<string, List<MessageListener>>();

        public void Dispatch(Message msg)
        {
            if (listeners.TryGetValue(msg.Name, out List<MessageListener> l))
            {
                for (int i = 0; i < l.Count; ++i)
                {
                    l[i].Handle(msg);
                }
            }
        }

        public void Add(MessageListener listener)
        {
            if (!listeners.TryGetValue(listener.MessageName, out List<MessageListener> l))
            {
                l = new List<MessageListener>();
                listeners[listener.MessageName] = l;
            }
            l.Add(listener);
        }

        public void Remove(MessageListener listener)
        {
            if (listeners.TryGetValue(listener.MessageName, out List<MessageListener> l))
            {
                l.Remove(listener);
            }
        }
    }
}