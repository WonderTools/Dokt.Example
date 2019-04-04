using System.Net;
using System.Net.Http;
using NUnit.Framework;
using WonderTools.Dokt;

namespace Dokt.Example.Tests
{
    public class Tests
    {
        private HttpClient _client;
        private DoktHttpMessageHandler _messageHandler;

        [SetUp]
        public void Setup()
        {
            _messageHandler=new DoktHttpMessageHandler();
            _client = new HttpClient(_messageHandler);
        }

        [Test]
        public void When_get_request_is_made_with_id_as_parameter_then_value_should_be_return()
        {
            var url = "https://localhost:44311/api/values/5";
            var responseContent = "value";
            _messageHandler.WhenRequest().WithUri(url).Respond(responseContent)
                .UsingStatusCode(HttpStatusCode.Accepted);

            var response = _client.GetAsync(url).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(responseContent,content);
        }
    }
}