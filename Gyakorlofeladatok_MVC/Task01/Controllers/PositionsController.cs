using Microsoft.AspNetCore.Mvc;
using Task01.Data;
using Task01.Models;

namespace Task01.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IPositionRepository repository;

        public PositionsController(IPositionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Position position)
        {
            repository.Add(position);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Position> positions = repository.GetAll();
            return View(positions);
        }
    }
}
