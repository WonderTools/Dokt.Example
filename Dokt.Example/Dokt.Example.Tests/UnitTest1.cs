using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Dokt.Example.Controllers;
using NUnit.Framework;
using WonderTools.Dokt;

namespace Dokt.Example.Tests
{
    public class Tests
    {
        private HttpClient _client;
        private DoktHttpMessageHandler _messageHandler;
        private ValuesController _valuesController;

        [SetUp]
        public void Setup()
        {
            _messageHandler=new DoktHttpMessageHandler();
            _client = new HttpClient(_messageHandler);
        }

        [Test]
        public void When_get_request_is_made_with_id_as_parameter_then_value_should_be_return()
        {
            var url = "http://google.com/search";
            _messageHandler
                .WhenRequest().WithUri(url)
                .Respond().UsingStatusCode(HttpStatusCode.Accepted);
            
            _valuesController = new ValuesController(_client);

            var result =  _valuesController.Get(5).Result;

            Assert.AreEqual("value",result.Value);
        }

        [Test]
        public void When_get_request_is_made_with_id_as_parameter_and_response_status_is_ok_then_value_should_be_return()
        {
            var url = "http://google.com/search";
            _messageHandler
                .WhenRequest().WithUri(url)
                .Respond().UsingStatusCode(HttpStatusCode.OK);

            _valuesController = new ValuesController(_client);

            var result = _valuesController.Get(5).Result;

            Assert.AreEqual("result is okay", result.Value);
        }

        [Test]
        public void When_post_request_is_made_then_request_result_should_be_made()
        {
            var url = @"http://google.com/search";
            var responseHttpCode = HttpStatusCode.OK;
            _messageHandler
                .WhenRequest()
                .WithUri(url)
                .Respond()
                .UsingStatusCode(responseHttpCode);

            _valuesController = new ValuesController(_client);

            var result = _valuesController.Post("defaultValue").Result;

            Assert.AreEqual("Content posted successfully", result.Value);
        }

    }
}