using WebApplication_REST.Models;

namespace WebApplication_REST.Data
{
    public interface IEmployeeRepository
    {
        void Create(Employee employee);
        void Delete(int id);
        IEnumerable<Employee> Read();
        Employee? Read(int id);
        void Update(Employee employee);
    }
}