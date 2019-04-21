using System.Net;
using System.Net.Http;

namespace Dokt.Example.Services
{
    public interface IService
    {
        string GetResult(int id);
        string PostContent(string content);
    }

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
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return "result is okay";
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return "result did not return anything";
            }
            return "value";
        }

        public string PostContent(string content)
        {
            var response = _client.PostAsync(@"http://google.com/search", new StringContent(content)).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return "Content posted successfully";
            }
            return "bad request";
        }
    }
}
