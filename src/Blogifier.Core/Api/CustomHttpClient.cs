using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.Api
{
    public class CustomHttpClient : HttpClient
    {
        public async Task<T> GetJsonAsync<T>(string requestUri, HttpClientHandler clientHandler)
        {
            HttpClient httpClient = new HttpClient(clientHandler);
            var httpContent = await httpClient.GetAsync(requestUri);
            string jsonContent = httpContent.Content.ReadAsStringAsync().Result;
            T obj = JsonConvert.DeserializeObject<T>(jsonContent);
            httpContent.Dispose();
            httpClient.Dispose();
            return obj;
        }
        public async Task<HttpResponseMessage> PostJsonAsync<T>(string requestUri, HttpClientHandler clientHandler, T content)
        {
            HttpClient httpClient = new HttpClient(clientHandler);
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUri, stringContent);
            httpClient.Dispose();
            return response;
        }
        public async Task<HttpResponseMessage> PutJsonAsync<T>(string requestUri, HttpClientHandler clientHandler, T content)
        {
            HttpClient httpClient = new HttpClient(clientHandler);
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(requestUri, stringContent);
            httpClient.Dispose();
            return response;
        }
    }
}
