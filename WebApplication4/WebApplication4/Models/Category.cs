using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication4.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter A Name")]
        public string? Name { get; set; }
        public ICollection<Item>? item { get; set; }
    }
}
