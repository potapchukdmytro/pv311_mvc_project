using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Models;
using pv311_mvc_project.Repositories.Categories;
using pv311_mvc_project.Repositories.Products;
using pv311_mvc_project.Services.Cart;
using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository, ICartService cartService)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cartService = cartService;
        }

        public IActionResult Index(string? category = "", int page = 1)
        {
            int pageSize = 12;

            var products = string.IsNullOrEmpty(category)
                    ? _productRepository.Products
                    : _productRepository.GetByCategory(category).Include(p => p.Category);

            int pagesCount = (int)Math.Ceiling(products.Count() / (double)pageSize);
            page = page <= 0 || page > pagesCount ? 1 : page;
            products = products.Skip((page - 1) * pageSize).Take(pageSize);

            var cartItems = _cartService.GetItems().Select(i => i.ProductId);

            var viewModel = new HomeProductsListVM
            {
                Products = products.Select(p => new HomeProductItemVM { Product = p, InCart = cartItems.Contains(p.Id) }),
                Categories = _categoryRepository.GetAll(),
                Page = page,
                PagesCount = pagesCount,
                Category = category ?? ""
            };

            return View(viewModel);
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
