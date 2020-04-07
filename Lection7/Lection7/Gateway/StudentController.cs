using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Gateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Gateway
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var host = _configuration.GetSection("Hosts:StudentApi").Value;

            var result = await new HttpClient().GetAsync(new Uri($"{host}/api/student/{id}"));

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            
            return Ok(await result.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentModel model)
        {
            var host = _configuration.GetSection("Hosts:StudentApi").Value;

            var content = new StreamContent(HttpContext.Request.Body);
            
            var result = await new HttpClient().PostAsync(new Uri($"{host}/api/student"), content);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            
            return Ok(await result.Content.ReadAsStringAsync());
        }
    }
}