using Microsoft.AspNetCore.Mvc;
using Task02.Data;
using Task02.Models;

namespace Task02.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository repository;

        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            repository.Add(product);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Product> products = repository.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult ByCategory(string category)
        {
            List<Product> products = repository.GetByCategory(category ?? string.Empty);
            ViewBag.SelectedCategory = category; // ViewBag is a dynamic object, same than ViewData["SelectedCategory"] = category. Important, that we can create any property in runtime, without need to declare it before.
            return View(products);
        }
    }
}
