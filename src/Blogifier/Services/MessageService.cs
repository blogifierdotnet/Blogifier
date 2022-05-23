using System;
using System.Threading.Tasks;

namespace Blogifier.Services
{
    public interface IMessageService
    {
        event Func<string, long, long, Task> OnMessage;
        Task SendMessage(string message, long commentIdforPosition = 0, long commentId = 0);
        Task ClearMessages();
        //bool MainEditorRendered { get; set; }
    }

    public class MessageService : IMessageService
    {
        public event Func<string, long, long, Task> OnMessage;

        public async Task SendMessage(string message, long commentIdforPosition = 0, long commentId = 0)
        {
            // System.Console.WriteLine(message);
            // System.Console.WriteLine(commentId);
            await OnMessage?.Invoke(message, commentIdforPosition, commentId);
        }

        public async Task ClearMessages()
        {
            await OnMessage?.Invoke(null, 0, 0);
        }
        //public bool MainEditorRendered { get; set; } = false;
    }
}