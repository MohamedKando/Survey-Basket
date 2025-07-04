using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Item


    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="aktp asm el item ya 5awl")]
        public string Name { get; set; }
        [Required(ErrorMessage = "aktp el s3r ya 5awl")]
        [Range(10,1200,ErrorMessage ="The Value of {0} must be between {1} and {2}")]
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        [ForeignKey("category")]
        public int categoryID { get; set; }
        public Category? category { get; set; }
    }
}
