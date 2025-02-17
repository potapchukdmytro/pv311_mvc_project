using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;
using pv311_mvc_project.Repositories.Products;
using pv311_mvc_project.Services.Image;
using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IProductRepository productRepository, IImageService imageService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepository;
            _imageService = imageService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            var categories = _context.Categories.AsEnumerable();

            var viewModel = new CreateProductVM
            {
                Categories = categories.Select(c =>
                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductVM viewModel)
        {
            string? fileName = null;

            if (viewModel.File != null)
            {
                fileName = await _imageService.SaveImageAsync(viewModel.File, Settings.PRODUCTS_PATH);
            }

            viewModel.Product.Image = fileName;
            viewModel.Product.Id = Guid.NewGuid().ToString();

            await _productRepository.CreateAsync(viewModel.Product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.FindByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Product model)
        {
            if(model.Image != null)
            {
                _imageService.DeleteImage(Path.Combine(Settings.PRODUCTS_PATH, model.Image));
            }

            await _productRepository.DeleteAsync(model.Id);

            return RedirectToAction("Index");
        }
    }
}
