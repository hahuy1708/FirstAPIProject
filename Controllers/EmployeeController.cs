using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPIProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Returning from EmployeeController GET Method";
        }
        [HttpGet]
        public string GetEmployee()
        {
            return "Returning from EmployeeController GETEMPLOYEE Method";
        }
    }
}
