using System.Net;
using System.Net.Http;
using Dokt.Example.Controllers;
using Dokt.Example.Services;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using WonderTools.Dokt;

namespace Dokt.Example.Tests
{
    public class Tests
    {
        private HttpClient _client;
        private DoktHttpMessageHandler _messageHandler;
        private IService _service;

        [SetUp]
        public void Setup()
        {
            _messageHandler=new DoktHttpMessageHandler();
            _client = new HttpClient(_messageHandler);
            _service = new TestService(_client);
        }

        [Test]
        public void When_get_request_is_made_with_id_as_parameter_then_value_should_be_return()
        {
            var url = "http://google.com/search";
            var body = "value";
            _messageHandler
                .WhenRequest().WithUri(url)
                .Respond(body).UsingStatusCode(HttpStatusCode.OK);

            var result = _service.GetResult(5);

            Assert.AreEqual(body,result);
        }

        [Test]
        public void When_get_request_is_made_with_id_as_parameter_and_response_status_is_other_than_ok_then_value_should_be_return()
        {
            var url = "http://google.com/search";
            _messageHandler
                .WhenRequest().WithUri(url)
                .Respond().UsingStatusCode(HttpStatusCode.Accepted);

            var result = _service.GetResult(5);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void When_post_request_is_made_then_request_result_should_be_displayed()
        {
            var url = @"http://google.com/search";
            var responseHttpCode = HttpStatusCode.OK;
            var content = "test";
            var responseBody = "Content posted successfully";
            _messageHandler
                .WhenPost()
                .WithUri(url)
                .WithContent(x=>x.ReadAsStringAsync().Result.Equals(content))
                .Respond(responseBody)
                .UsingStatusCode(responseHttpCode);

            var result = _service.PostContent(content);

            Assert.AreEqual(responseBody, result);
        }

        [Test]
        public void When_post_request_is_made_with_request_headers_then_request_result_should_be_returned()
        {
            var url = @"http://google.com/search";
            var responseHttpCode = HttpStatusCode.OK;
            var content = "test";
            var responseBody = "Content posted successfully";
            _messageHandler
                .WhenPost()
                .WithUri(url)
                .WithContent(x => x.ReadAsStringAsync().Result.Equals(content))
                .Respond(responseBody)
                .UsingStatusCode(responseHttpCode);

            var result = _service.PostContent(content);

            Assert.AreEqual(responseBody, result);
        }

    }
}