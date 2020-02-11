using System;
using VT.Messaging;

namespace VT.Front
{
    public class FrontAction : IDisposable
    {
        private MessageListener messageListener;
        private readonly string messageName;
        private static readonly MessageDispatcher dispatcher = new MessageDispatcher();


        public FrontAction(string messageName/*, Action<Message> messageHandler*/)
        {
            this.messageName = messageName;
        }

        public void Listen(Action<Message> message)
        {

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