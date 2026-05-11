using Task03.Models;

namespace Task03.Data
{
    public interface IStudentRepository
    {
        void Add(Student student);
        List<Student> GetAll();
        List<Student> GetByClass(string className);
    }
}
