using Microsoft.EntityFrameworkCore;
using Task01.Models;

namespace Task01.Data
{
    public class SchoolDbContext : DbContext
    {
        // Constructor receives DbContext configuration from Program.cs
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        // These properties represent database tables
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Seed initial data into the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Anna Kovacs",
                    Email = "anna.kovacs@example.com",
                    EnrollmentDate = new DateTime(2024, 9, 1),
                    IsActive = true
                },
                new Student
                {
                    Id = 2,
                    Name = "Peter Nagy",
                    Email = "peter.nagy@example.com",
                    EnrollmentDate = new DateTime(2023, 9, 1),
                    IsActive = true
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "Web Development",
                    Category = "Programming",
                    Credit = 5,
                    IsOnline = false
                },
                new Course
                {
                    Id = 2,
                    Title = "Database Systems",
                    Category = "IT",
                    Credit = 4,
                    IsOnline = true
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
