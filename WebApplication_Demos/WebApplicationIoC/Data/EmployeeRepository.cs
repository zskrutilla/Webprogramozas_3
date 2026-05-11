using WebApplication_IoC.Models;

namespace WebApplication_IoC.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        List<Employee> employers;

        public EmployeeRepository()
        {
            employers = new List<Employee>();
        }

        public void Create(Employee emp)
        {
            employers.Add(emp);
        }

        public void Delete(string name)
        {
            Employee? delete = this.Read(name);
            if (delete != null)
            {
                employers.Remove(delete);
            }
        }

        public IEnumerable<Employee> Read()
        {
            return employers;
        }

        public Employee? Read(string name)
        {
            return employers.FirstOrDefault(e => e.Name == name);
        }

        public void Update(Employee emp)
        {
            Employee? update = this.Read(emp.Name!);
            if (update != null)
            {
                update.Name = emp.Name;
                update.IdNumber = emp.IdNumber;
                update.Department = emp.Department;
                update.Position = emp.Position;
            }
        }
    }
}
