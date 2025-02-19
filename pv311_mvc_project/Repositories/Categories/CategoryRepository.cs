using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Data;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Repositories.Categories
{
    public class CategoryRepository
        : GenericRepository<Category, string>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context) { }

        public IQueryable<Category> Categories => GetAll().Include(c => c.Products);
    }
}
