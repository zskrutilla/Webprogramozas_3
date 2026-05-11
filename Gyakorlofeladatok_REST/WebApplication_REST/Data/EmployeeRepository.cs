using WebApplication_REST.Models;

namespace WebApplication_REST.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeDbContext db;

        public EmployeeRepository(EmployeeDbContext employeeDb)
        {
            db = employeeDb;
        }

        public void Create(Employee employee)
        {
            db.Add(employee);
            db.SaveChanges();
        }

        public void Update(Employee employee)
        {
            Employee? employeeUpdate = Read(employee.ID);
            if (employeeUpdate != null)
            {
                employeeUpdate.Position = employee.Position;
                employeeUpdate.IsActive = employee.IsActive;
                employeeUpdate.Name = employee.Name;
                employeeUpdate.Salary = employee.Salary;
            }
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Employee? employeeDelete = Read(id);
            if (employeeDelete != null)
            {
                db.Employees.Remove(employeeDelete);
            }
            db.SaveChanges();
        }

        public IEnumerable<Employee> Read()
        {
            return db.Employees;
        }

        public Employee? Read(int id)
        {
            return db.Employees.FirstOrDefault(x => x.ID == id);
        }
    }
}
