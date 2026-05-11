using Microsoft.AspNetCore.Mvc;
using Task03.Data;
using Task03.Models;

namespace Task03.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassRepository classRepository;

        public ClassesController(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SchoolClass schoolClass)
        {
            classRepository.Add(schoolClass);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            List<SchoolClass> classes = classRepository.GetAll();
            return View(classes);
        }
    }
}
