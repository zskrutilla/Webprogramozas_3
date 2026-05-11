using Microsoft.AspNetCore.Mvc;
using Task03.Data;
using Task03.Models;

namespace Task03.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IClassRepository classRepository;

        public StudentsController(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            this.studentRepository = studentRepository;
            this.classRepository = classRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Classes = classRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            studentRepository.Add(student);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Student> students = studentRepository.GetAll();
            return View(students);
        }

        [HttpGet]
        public IActionResult ByClass(string className)
        {
            List<Student> students = studentRepository.GetByClass(className ?? string.Empty);
            ViewBag.SelectedClass = className; // Same than ViewData["SelectedClass"] = className;
            return View(students);
        }
    }
}
