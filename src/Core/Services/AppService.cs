using Microsoft.Extensions.Options;

namespace Core.Services
{
    public interface IAppService<out T> : IOptionsSnapshot<T> where T : class, new()
    {
    }

    public class AppService<T> : IAppService<T> where T : class, new()
    {
        private readonly IOptionsMonitor<T> _options;

        public AppService(IOptionsMonitor<T> options)
        {
            _options = options;
        }

        public T Value => _options.CurrentValue;
        public T Get(string name) => _options.Get(name);
    }
}