using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Device : BaseEntity
    {
        [MaxLength(50)]
        public string icon { get; set; } = string.Empty;  
        public ICollection<GameDevice> games { get; set; }
    }
}
