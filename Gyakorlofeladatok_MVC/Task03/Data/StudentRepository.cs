using Task03.Models;

namespace Task03.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> students = new();

        public StudentRepository()
        {
            students.Add(new Student { Name = "Anna", Age = 20, ClassName = "A1" });
            students.Add(new Student { Name = "Peter", Age = 21, ClassName = "A1" });
            students.Add(new Student { Name = "Laura", Age = 19, ClassName = "B2" });
        }

        public void Add(Student student)
        {
            if (student != null &&
                !string.IsNullOrWhiteSpace(student.Name) &&
                !string.IsNullOrWhiteSpace(student.ClassName))
            {
                students.Add(student);
            }
        }

        public List<Student> GetAll()
        {
            return students;
        }

        public List<Student> GetByClass(string className)
        {
            return students
                .Where(s => s.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
