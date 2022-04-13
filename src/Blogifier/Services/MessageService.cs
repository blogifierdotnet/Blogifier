using System;

namespace Blogifier.Services
{
    public interface IMessageService
    {
        event Action<string> OnMessage;
        void SendMessage(string message);
        void ClearMessages();
    }

    public class MessageService : IMessageService
    {
        public event Action<string> OnMessage;

        public void SendMessage(string message)
        {
            OnMessage?.Invoke(message);
        }

        public void ClearMessages()
        {
            OnMessage?.Invoke(null);
        }
    }
}