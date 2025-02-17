using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Data
{
    public class AppDbContext(DbContextOptions options) 
        : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
