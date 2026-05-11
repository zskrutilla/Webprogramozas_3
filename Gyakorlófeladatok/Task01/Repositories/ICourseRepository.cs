using Task01.Models;

namespace Task01.Repositories
{
    public interface ICourseRepository
    {
        List<Course> ReadAll();
        Course? ReadById(int id);
        void Create(Course course);
        void Update(Course course);
        void Delete(int id);
    }
}
