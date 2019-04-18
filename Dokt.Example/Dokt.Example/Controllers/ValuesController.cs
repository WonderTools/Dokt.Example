using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dokt.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly HttpClient _client;

        public ValuesController(HttpClient client)
        {
            _client = client;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var response =  _client.GetAsync("http://google.com/search").Result;
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

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] string value)
        {
            var response = _client.PostAsync(@"http://google.com/search", new StringContent("test")).Result;
            if (response.StatusCode==HttpStatusCode.OK)
            {
                return "Content posted successfully";
            }

            return "bad request";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
