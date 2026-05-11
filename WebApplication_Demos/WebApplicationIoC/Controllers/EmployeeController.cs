using WebApplication_IoC.Data;
using WebApplication_IoC.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_IoC.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository EmployeeRepository;
        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            this.EmployeeRepository = EmployeeRepository;
        }

        public IActionResult Index()
        {
            return View(this.EmployeeRepository.Read()); // read all
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            this.EmployeeRepository.Create(emp);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(string name)
        {
            this.EmployeeRepository.Delete(name);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(string name)
        {
            Employee? emp = EmployeeRepository.Read(name);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Update(Employee emp)
        {
            this.EmployeeRepository.Update(emp);
            return RedirectToAction(nameof(Index));
        }
    }
}
