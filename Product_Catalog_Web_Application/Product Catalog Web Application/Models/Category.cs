using System.ComponentModel.DataAnnotations;
using static Product_Catalog_Web_Application.Models.Products;

namespace Product_Catalog_Web_Application.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Products> products { get; set; }
    }
}
