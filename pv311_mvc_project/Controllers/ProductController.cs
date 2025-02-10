using Microsoft.AspNetCore.Mvc;

namespace pv311_mvc_project.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
