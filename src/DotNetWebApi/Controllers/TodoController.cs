using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetWebApi.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TodoController : Controller
    {

        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            _logger.LogInformation("Get Todos");
            return Ok(new object[] { 1, 2, 3 });
        }

        [HttpPost]
        public IActionResult Post([FromBody]object input)
        {
            return Created(nameof(Get), input);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] object input)
        {
            return Ok();
        }
    }
}
