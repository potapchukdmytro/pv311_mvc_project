using Microsoft.AspNetCore.Mvc;
using pv311_mvc_project.Services.Cart;
using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItemVM viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.ProductId))
                return BadRequest();

            _cartService.AddToCart(viewModel);
            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveFromCart([FromBody] CartItemVM viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.ProductId))
                return BadRequest();

            _cartService.RemoveFromCart(viewModel);
            return Ok();
        }
    }
}
