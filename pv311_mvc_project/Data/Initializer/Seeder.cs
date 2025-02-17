using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Data.Initializer
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                context.Database.Migrate();

                if (!context.Categories.Any())
                {
                    var categories = new List<Category>
                    {
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Процесори" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Материнські плати" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Блоки живлення" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Оперативна пам'ять" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Відеокарти" },
                        new Category{ Id = Guid.NewGuid().ToString(), Name = "Накопичувачі" }
                    };

                    context.Categories.AddRange(categories);
                    context.SaveChanges();

                    var products = new List<Product>
                    {
                        new Product { Id = Guid.NewGuid().ToString(), Name = "Gigabyte GeForce RTX 4060 Gaming OC 8192MB", Price = 15799, Amount = 5,CategoryId = categories[4].Id },
                        new Product { Id = Guid.NewGuid().ToString(), Name = "Gigabyte B550M AORUS ELITE AX", Price = 4699, Amount = 10, CategoryId = categories[1].Id }
                    };

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }
        }
    }
}
