using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Catalog_Web_Application.Models
{
    public class Products
    {
        
            [Key]
            
            public int Id { get; set; }
            [Required(ErrorMessage ="Please Enter The Product Name")]
            public string Name { get; set; }

            public DateTime CreationDate { get; set; } = DateTime.Now;
            
            [Required(ErrorMessage = "Please Enter The Date You Want This Item To Display")]
            public DateTime StartDate { get; set; }
            [Required(ErrorMessage = "Please Enter The Display Duration For This Item")]
            public int Duration { get; set; } // Duration in days
            [Required(ErrorMessage = "Please Enter The Price")]
            public decimal Price { get; set; }
            [ForeignKey(nameof(category))]
            public int? CategoryId { get; set; }
            public Category? category { get; set; }
            

            public string? CreatedByUserId { get; set; }
            public IdentityUser? user { get; set; }
    }
    
}
