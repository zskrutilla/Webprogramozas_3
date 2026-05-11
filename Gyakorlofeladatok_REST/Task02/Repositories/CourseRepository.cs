using Task02.Data;
using Task02.Models;

namespace Task02.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext context;

        public CourseRepository(SchoolDbContext context)
        {
            this.context = context;
        }

        public List<Course> ReadAll()
        {
            return context.Courses.ToList();
        }

        public Course? ReadById(int id)
        {
            return context.Courses.FirstOrDefault(c => c.Id == id);
        }

        public void Create(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Update(Course course)
        {
            var oldCourse = context.Courses.FirstOrDefault(c => c.Id == course.Id);

            if (oldCourse != null)
            {
                oldCourse.Title = course.Title;
                oldCourse.Category = course.Category;
                oldCourse.Credit = course.Credit;
                oldCourse.IsOnline = course.IsOnline;

                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var course = context.Courses.FirstOrDefault(c => c.Id == id);

            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
            }
        }
    }
}
