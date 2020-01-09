using System;
using VT.Messaging;

namespace VT.Front
{
    public class FrontAction : IDisposable
    {
        private readonly string messageName;
        private readonly Action<Message> messageHandler;
        private readonly MessageListener messageListener;
        private static readonly MessageDispatcher dispatcher = new MessageDispatcher();


        public FrontAction(string messageName, Action<Message> messageHandler)
        {
            this.messageName = messageName;
            this.messageHandler = messageHandler;
            messageListener = new MessageListener(dispatcher, messageName, messageHandler);
        }

        internal static void Dispatch(Message message)
        {
            dispatcher.Dispatch(message);
        }

        public void Dispose()
        {
            messageListener.Dispose();
        }
    }
}