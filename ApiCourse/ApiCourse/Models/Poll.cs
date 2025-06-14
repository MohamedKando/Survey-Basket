namespace ApiCourse.Models
{
    public class Poll : AuditableEntity
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }=string.Empty;
        public string Summary { get; set; } = string.Empty;

        public bool IsPublished { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
    }
}
