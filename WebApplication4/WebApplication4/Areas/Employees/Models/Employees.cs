using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Areas.Employees.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
     
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }

    }
}
