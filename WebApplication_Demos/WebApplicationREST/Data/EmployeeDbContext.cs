using WebApplication_REST.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_REST.Data
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmployeeDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EmployeeDB;Integrated Security=True;MultipleActiveResultSets=True";
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    ID = 1,
                    Name = "Test",
                    Hired = DateTime.Now,
                    IsActive = true,
                    Position = Positions.Developer,
                    Salary = 65445
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
