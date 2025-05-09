using FirstAPIProjec.Models;
using FirstAPIProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        //  Retrieves all employees (GET api/employee).
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = _repository.GetAll();
            return Ok(employees);
        }
        //Retrieves a specific employee by ID(GET api/employee/{id}).
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeByID(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }
        
        //Adds a new employee(POST api/employee).
        [HttpPost]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            _repository.Add(employee);
            return CreatedAtAction(nameof(GetEmployeeByID), new { id = employee.Id }, employee);
        }
        
        // Updates an existing employee entirely (PUT api/employee/{id}).
        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if(id != employee.Id) return BadRequest("Employee id mismatch");
            if(!_repository.Exists(id)) return NotFound();
            _repository.Update(employee);
            return NoContent();
        }
        
        // Partially updates an existing employee (PATCH api/employee/{id}).
        [HttpPatch("{id}")]
        public IActionResult PatchEmployee(int id, [FromBody] Employee employee)
        {
            var existingEmployee = _repository.GetById(id);
            if(existingEmployee == null) return NotFound();
            // For simplicity, updating all fields. In real scenarios, use JSON Patch.
            existingEmployee.Name = employee.Name ?? existingEmployee.Name;
            existingEmployee.Email = employee.Email ?? existingEmployee.Email;
            existingEmployee.Position = employee.Position ?? existingEmployee.Position;
            existingEmployee.Age = employee.Age != 0 ? employee.Age : existingEmployee.Age;
            _repository.Update(existingEmployee);
            return NoContent();
            // For each property, update the existing value only if a new value is provided:
            // - For string properties (Name, Email, Position), use the null-coalescing operator (??)
            //   to keep the current value when the new value is null.
            // - For Age, update only if the new age is non-zero; otherwise, retain the existing age.
        }
        
        // Deletes an employee (DELETE api/employee/{id}).
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (!_repository.Exists(id))
            {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }
        
        
    }
}
