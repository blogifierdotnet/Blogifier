using System;
using System.Threading.Tasks;

namespace Blogifier.Services
{
    public interface IMessageService
    {
        event Func<string, Task> OnMessage;
        Task SendMessage(string message);
        Task ClearMessages();
    }

    public class MessageService : IMessageService
    {
        public event Func<string, Task> OnMessage;

        public async Task SendMessage(string message)
        {
            await OnMessage?.Invoke(message);
        }

        public async Task ClearMessages()
        {
            await OnMessage?.Invoke(null);
        }
    }
}