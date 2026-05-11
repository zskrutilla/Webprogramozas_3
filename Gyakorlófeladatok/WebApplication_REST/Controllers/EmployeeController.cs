using WebApplication_REST.Data;
using WebApplication_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_REST.Controllers
{
    [ApiController] // This will be a sign for the MVC that it will be API-based communication endpoints
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        // GET: api/employee
        [HttpGet] // The controller will be working with HTTP requests therefore we need use attributes
        public IEnumerable<Employee> GetEmployees()
        {
            return employeeRepository.Read();
        }

        // --------------------------- Further functions ---------------------------

        // GET: api/employee/5
        [HttpGet("{id}")]
        public Employee? GetEmployees(int id)
        {
            return employeeRepository.Read(id);
        }

        // POST: api/employee
        // [FromBody] means the data comes from the HTTP request body (JSON)
        // The framework converts the JSON into an Employee object automatically
        [HttpPost]
        public void CreateEmployee([FromBody] Employee employee)
        {
            employeeRepository.Create(employee);
        }

        // PUT: api/employee
        [HttpPut]
        public void EditEmployee([FromBody] Employee employee)
        {
            employeeRepository.Update(employee);
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public void DeleteEmployee(int id)
        {
            employeeRepository.Delete(id);
        }
    }
}
