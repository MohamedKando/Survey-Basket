
namespace ApiCourse.Persistence.EntitesConfigurations
{
    public class QuestionConfigurations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.PollId, x.Content }).IsUnique();
            builder.Property(x => x.Content).HasMaxLength(1000);
        }
    }
}
