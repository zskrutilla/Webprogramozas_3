using Microsoft.EntityFrameworkCore;
using WebApplication_REST.Models;

namespace WebApplication_REST.Data
{
    public class EmployeeDbContext : DbContext
    {
        // Constructor receives configuration from Program.cs
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        // Represents the Employees table in the database
        public DbSet<Employee> Employees { get; set; }

        // This method is used to configure the database model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HasData() is used to insert initial (seed) data into the database
            // This data will be created when the database is first created
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    ID = 1,
                    Name = "Test User",
                    Hired = new DateTime(2024, 1, 1),
                    IsActive = true,
                    Position = Positions.Developer,
                    Salary = 65445
                }
            );

            // Call base method (good practice)
            base.OnModelCreating(modelBuilder);
        }
    }
}