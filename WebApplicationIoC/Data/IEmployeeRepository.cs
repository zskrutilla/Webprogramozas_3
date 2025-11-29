using WebApplication_IoC.Models;

namespace WebApplication_IoC.Data
{
    public interface IEmployeeRepository
    {
        void Create(Employee emp);
        void Delete(string name);
        IEnumerable<Employee> Read();
        Employee? Read(string name);
        void Update(Employee emp);
    }
}
