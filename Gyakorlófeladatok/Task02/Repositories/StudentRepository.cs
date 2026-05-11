using Task02.Data;
using Task02.Models;

namespace Task02.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext context;

        public StudentRepository(SchoolDbContext context)
        {
            this.context = context;
        }

        public List<Student> ReadAll()
        {
            return context.Students.ToList();
        }

        public Student? ReadById(int id)
        {
            return context.Students.FirstOrDefault(s => s.Id == id);
        }

        public void Create(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }

        public void Update(Student student)
        {
            var oldStudent = context.Students.FirstOrDefault(s => s.Id == student.Id);

            if (oldStudent != null)
            {
                oldStudent.Name = student.Name;
                oldStudent.Email = student.Email;
                oldStudent.EnrollmentDate = student.EnrollmentDate;
                oldStudent.IsActive = student.IsActive;

                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var student = context.Students.FirstOrDefault(s => s.Id == id);

            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
        }
    }
}
