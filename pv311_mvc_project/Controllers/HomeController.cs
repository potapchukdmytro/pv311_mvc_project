using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pv311_mvc_project.Models;
using pv311_mvc_project.Repositories.Products;

namespace pv311_mvc_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.Products;
            return View(products);
        }

        [ActionName("Details")]
        public IActionResult ProductDetails(string? id)
        {
            if (id == null)
                return NotFound();

            return View("ProductDetails");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
