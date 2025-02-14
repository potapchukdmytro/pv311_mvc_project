using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;
using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsEnumerable();

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
        public IActionResult Create([FromForm] CreateProductVM viewModel)
        {
            string? fileName = null;

            if (viewModel.File != null)
            {
                fileName = SaveImage(viewModel.File);
            }

            viewModel.Product.Image = fileName;
            viewModel.Product.Id = Guid.NewGuid().ToString();

            _context.Products.Add(viewModel.Product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product model)
        {
            if(model.Image != null)
            {
                string imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, Settings.PRODUCTS_PATH);
                string imagePath = Path.Combine(imagesPath, model.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Products.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private string? SaveImage(IFormFile file)
        {
            try
            {
                var types = file.ContentType.Split("/");

                if (types[0] != "image")
                {
                    return null;
                }

                string imageName = $"{Guid.NewGuid()}.{types[1]}";
                string imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, Settings.PRODUCTS_PATH);
                string imagePath = Path.Combine(imagesPath, imageName);

                using (var fileStream = System.IO.File.Create(imagePath))
                {
                    using (var stream = file.OpenReadStream())
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                return imageName;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
