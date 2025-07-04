using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class Game : BaseEntity
    {

        [MaxLength(2550)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        [ForeignKey(nameof(category))]
        public int Categoryid { get; set; }
        public Category category { get; set; } = default!;
        public ICollection<GameDevice> Device { get; set;} = new List<GameDevice>();

    }
}
