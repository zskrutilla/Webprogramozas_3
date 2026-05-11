using Task02.Models;

namespace Task02.Repositories
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
