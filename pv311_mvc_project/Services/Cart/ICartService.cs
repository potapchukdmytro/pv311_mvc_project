using pv311_mvc_project.ViewModels;

namespace pv311_mvc_project.Services.Cart
{
    public interface ICartService
    {
        void AddToCart(CartItemVM viewModel);
        void RemoveFromCart(CartItemVM viewModel);
        IEnumerable<CartItemVM> GetItems();
    }
}
