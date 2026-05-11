using Task02.Models;

namespace Task02.Repositories
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
