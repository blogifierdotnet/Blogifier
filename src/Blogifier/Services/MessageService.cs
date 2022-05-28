using System;
using System.Threading.Tasks;

namespace Blogifier.Services
{
    public interface IMessageService
    {
        event Func<string, long, long, Task> OnMessageAsync;
        Task SendMessage(string message, long commentIdforPosition = 0, long commentId = 0);
        Task ClearMessages();
        //bool MainEditorRendered { get; set; }
    }

    public class MessageService : IMessageService
    {
        public event Func<string, long, long, Task> OnMessageAsync;

        public async Task SendMessage(string message, long commentIdforPosition = 0, long commentId = 0)
        {
            // System.Console.WriteLine(message);
            // System.Console.WriteLine(commentId);
            await OnMessageAsync?.Invoke(message, commentIdforPosition, commentId);
        }

        public async Task ClearMessages()
        {
            await OnMessageAsync?.Invoke(null, 0, 0);
        }
        //public bool MainEditorRendered { get; set; } = false;
    }
}