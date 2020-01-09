using System;
using UnityEngine;

namespace VT.Messaging
{
    public class MessageListener : IDisposable
    {
        public string MessageName { get; private set; }

        private readonly Action<Message> messageHandler;
        private readonly MessageDispatcher dispatcher;
        private readonly MessageFilter filter;

        public MessageListener(MessageDispatcher dispatcher, string messageName, Action<Message> messageHandler) : this(dispatcher, messageName, new MessageFilter(), messageHandler)
        {
        }

        public MessageListener(MessageDispatcher dispatcher, string messageName, MessageFilter filter, Action<Message> messageHandler)
        {
            MessageName = messageName;
            this.messageHandler = messageHandler;
            this.dispatcher = dispatcher;
            this.filter = filter;
            dispatcher.Add(this);
        }

        public void Handle(Message msg)
        {
            if (!filter.Accept(msg))
            {
                return;
            }

            try
            {
                messageHandler.Invoke(msg);
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void Dispose()
        {
            dispatcher.Remove(this);
        }
    }
}