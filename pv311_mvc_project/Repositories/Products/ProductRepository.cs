using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Product model)
        {
            await _context.Products.AddAsync(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var model = await FindByIdAsync(id);

            if (model == null)
                return false;

            _context.Products.Remove(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Product?> FindByIdAsync(string id)
        {
            var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return model;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var models = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return models;
        }

        public async Task<bool> UpdateAsync(Product model)
        {
            _context.Products.Update(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
