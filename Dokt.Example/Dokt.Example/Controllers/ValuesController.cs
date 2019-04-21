using System.Collections.Generic;
using System.Threading.Tasks;
using Dokt.Example.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dokt.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IService _service;

        public ValuesController(IService service)
        {
            _service = service;
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
           return _service.GetResult(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] string value)
        {
            return _service.PostContent(value);
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
