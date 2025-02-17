using Microsoft.AspNetCore.Mvc;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;
using pv311_mvc_project.Validators;

namespace pv311_mvc_project.Controllers
{
    public class CategoryController(AppDbContext context) 
        : Controller
    {
        public IActionResult Index()
        {
            var categories = context.Categories.AsEnumerable();
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
            var validator = new CategoryValidator();
            var result = validator.Validate(model);
            
            if(!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            model.Id = Guid.NewGuid().ToString();
            context.Categories.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var category = context.Categories.FirstOrDefault(c => c.Id == id);

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
            context.Categories.Update(model);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = context.Categories.FirstOrDefault(c => c.Id == id);

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
            context.Categories.Remove(model);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
