using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services;
namespace WebApplication2.Controllers.Implementation
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleAPIController : Controller
    {
        private readonly ISampleAPIService _service;

        public SampleAPIController(ISampleAPIService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AddEmployee([FromQuery] Models.Employee employee)
        {
            _service.AddEmployee(employee);
            return Ok(employee);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _service.Get();
            return Ok(employees);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string name)
        {
            _service.Delete(name);
            var employees = _service.Get();
            return Ok(employees);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string search)
        {
            Dictionary<string, Employee> employees = _service.Search(search);
            return Ok(employees);
        }
    }

}
