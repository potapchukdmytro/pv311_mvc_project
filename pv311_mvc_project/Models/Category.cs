using System.ComponentModel.DataAnnotations;

namespace pv311_mvc_project.Models
{
    public class Category : BaseModel<string>
    {
        [Required, MaxLength(100)]
        public string? Name { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}
