using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPIProject.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Route("Employee/All")]
        [HttpGet]
        public string GetAll()
        {   
            return "Response from GetAll Method";
        }
        
        [Route("Employee/ByID/{id}")]
        [HttpGet]
        public string GetEmployeeByID(int id)
        {
            return $"Response from GetEmployeeById Method Id: {id}";
        }
    }
}
