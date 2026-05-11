using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task02.Models;
using Task02.Repositories;

namespace Task02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository repository;

        public StudentController(IStudentRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/student
        [HttpGet]
        public List<Student> GetAll()
        {
            return repository.ReadAll();
        }

        // GET: api/student/5
        [HttpGet("{id}")]
        public Student? GetById(int id)
        {
            return repository.ReadById(id);
        }

        // POST: api/student
        [HttpPost]
        public void Create([FromBody] Student student)
        {
            // [FromBody] means the data comes from the HTTP request body
            repository.Create(student);
        }

        // PUT: api/student
        [HttpPut]
        public void Update([FromBody] Student student)
        {
            repository.Update(student);
        }

        // DELETE: api/student/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
