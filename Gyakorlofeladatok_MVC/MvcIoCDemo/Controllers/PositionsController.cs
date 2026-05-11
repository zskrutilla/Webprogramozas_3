using Microsoft.AspNetCore.Mvc;
using MvcIoCDemo.Data;
using MvcIoCDemo.Models;

namespace MvcIoCDemo.Controllers;

public class PositionsController : Controller
{
    private readonly IPositionRepository repository;
    //private readonly List<Position> positions;

    public PositionsController(IPositionRepository repository)
    {
        this.repository = repository;
        //positions = new List<Position>();
        //positions.Add(new Position() { Id = 1, Name = "Developer" });
        //positions.Add(new Position() { Id = 2, Name = "Tester" });
        //positions.Add(new Position() { Id = 3, Name = "Project Manager" });
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(string positionName)
    {
        repository.Add(positionName);
        //positions.Add(new Position() { Id = 4, Name = positionName });
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult List()
    {
        var model = repository.GetAll();
        return View(model);
        //return View(positions);
    }
}
