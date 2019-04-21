using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Dokt.Example.Services
{
    public class TestService:IService
    {
        private readonly HttpClient _client;

        public TestService()
        {
            _client = new HttpClient();
        }

        public TestService(HttpClient client)
        {
            _client = client;
        }

        public string GetResult(int id)
        {
            var response = _client.GetAsync("http://google.com/search").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return content;
            }

            return string.Empty;
        }

        public string PostContent(string content)
        {
            var response = _client.PostAsync(@"http://google.com/search", new StringContent(content)).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return result;
            }
            return string.Empty;
        }
    }
}
