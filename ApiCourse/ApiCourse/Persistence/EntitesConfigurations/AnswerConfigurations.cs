
namespace ApiCourse.Persistence.EntitesConfigurations
{
    public class AnswerConfigurations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new {x.QuestionId , x.Content}).IsUnique();
            builder.Property(x => x.Content).HasMaxLength(1000 );

        }
    }
}
