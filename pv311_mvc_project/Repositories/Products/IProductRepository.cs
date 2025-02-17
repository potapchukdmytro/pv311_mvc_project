using pv311_mvc_project.Models;

namespace pv311_mvc_project.Repositories.Products
{
    public interface IProductRepository
    {
        Task<bool> CreateAsync(Product model);
        Task<bool> UpdateAsync(Product model);
        Task<bool> DeleteAsync(string id);
        Task<List<Product>> GetAllAsync();
        Task<Product?> FindByIdAsync(string id);
    }
}