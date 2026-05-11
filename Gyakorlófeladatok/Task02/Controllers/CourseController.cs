using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task02.Models;
using Task02.Repositories;

namespace Task02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository repository;

        public CourseController(ICourseRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/course
        [HttpGet]
        public List<Course> GetAll()
        {
            return repository.ReadAll();
        }

        // GET: api/course/5
        [HttpGet("{id}")]
        public Course? GetById(int id)
        {
            return repository.ReadById(id);
        }

        // POST: api/course
        [HttpPost]
        public void Create([FromBody] Course course)
        {
            // [FromBody] means the data comes from the HTTP request body
            repository.Create(course);
        }

        // PUT: api/course
        [HttpPut]
        public void Update([FromBody] Course course)
        {
            repository.Update(course);
        }

        // DELETE: api/course/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
