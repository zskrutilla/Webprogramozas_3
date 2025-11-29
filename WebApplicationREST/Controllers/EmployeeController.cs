using WebApplication_REST.Data;
using WebApplication_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_REST.Controllers
{
    [ApiController] // This will be a sign for the MVC that it will be API-based communication endpoints
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet] // The controller will be working with HTTP requests therefore we need use attributes
        public IEnumerable<Employee> GetEmployees()
        {
            return employeeRepository.Read();
        }
        // Further functions
        [HttpGet("{id}")]
        public Employee? GetEmployees(int id)
        {
            return employeeRepository.Read(id);
        }
        [HttpPost]
        public void CreateEmployee([FromBody] Employee employee)
        {
            employeeRepository.Create(employee);
        }
        [HttpPut]
        public void EditEmployee([FromBody] Employee employee)
        {
            employeeRepository.Update(employee);
        }
        [HttpDelete("{id}")]
        public void DeleteEmployee(int id)
        {
            employeeRepository.Delete(id);
        }
    }
}
