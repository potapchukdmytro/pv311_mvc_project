using Microsoft.AspNetCore.Mvc;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.AsEnumerable();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            model.Id = Guid.NewGuid().ToString();
            _context.Categories.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category model)
        {
            _context.Categories.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category model)
        {
            _context.Categories.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
