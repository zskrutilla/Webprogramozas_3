using Task01.Models;

namespace Task01.Repositories
{
    public interface IStudentRepository
    {
        List<Student> ReadAll();
        Student? ReadById(int id);
        void Create(Student student);
        void Update(Student student);
        void Delete(int id);
    }
}
