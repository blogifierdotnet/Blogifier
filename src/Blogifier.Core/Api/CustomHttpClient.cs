using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Api
{
    public class CustomHttpClient : HttpClient
    {
        private readonly ILogger<CustomHttpClient> _logger;

        public CustomHttpClient(ILogger<CustomHttpClient> logger)
        {
            _logger = logger;
        }
        
        public async Task<T> GetJsonAsync<T>(string requestUri, HttpRequest request)
        {
            _logger.LogInformation($"GetJsonAsync - {BaseAddress} - {requestUri}");
            HttpClientHandler clientHandler = GetHttpHandler(request);
            HttpClient httpClient = new HttpClient(clientHandler);
            var httpContent = await httpClient.GetAsync($"{BaseAddress}{requestUri}");
            string jsonContent = httpContent.Content.ReadAsStringAsync().Result;
            T obj = JsonConvert.DeserializeObject<T>(jsonContent);
            httpContent.Dispose();
            httpClient.Dispose();
            return obj;
        }
        public async Task<HttpResponseMessage> PostJsonAsync<T>(string requestUri, HttpRequest request, T content)
        {
            HttpClientHandler clientHandler = GetHttpHandler(request);
            HttpClient httpClient = new HttpClient(clientHandler);
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseAddress}{requestUri}", stringContent);
            httpClient.Dispose();
            return response;
        }
        public async Task<HttpResponseMessage> PutJsonAsync<T>(string requestUri, HttpRequest request, T content)
        {
            HttpClientHandler clientHandler = GetHttpHandler(request);
            HttpClient httpClient = new HttpClient(clientHandler);
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{BaseAddress}{requestUri}", stringContent);
            httpClient.Dispose();
            return response;
        }

        public async Task<HttpResponseMessage> RemoveAsync(string requestUri, HttpRequest request)
        {
            HttpClientHandler clientHandler = GetHttpHandler(request);
            HttpClient httpClient = new HttpClient(clientHandler);
            var response = await httpClient.DeleteAsync($"{BaseAddress}{requestUri}");
            httpClient.Dispose();
            return response;
        }

        private HttpClientHandler GetHttpHandler(HttpRequest request)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseCookies = true;
            clientHandler.CookieContainer = new CookieContainer();

            if (request.Cookies != null)
            {
                var cookieVal = request.Cookies[Constants.IdentityCookieName];
                if (cookieVal != null)
                {
                    clientHandler.CookieContainer.Add(new Cookie
                    {
                        Name = Constants.IdentityCookieName,
                        Value = cookieVal,
                        Expires = DateTime.Now.AddDays(30),
                        Domain = request.Host.Host
                    });
                }
            }
            return clientHandler;
        }
    }
}
