namespace VT.Messaging
{
    public interface IMessageFilter
    {
        bool Accept(Message msg);
    }
}