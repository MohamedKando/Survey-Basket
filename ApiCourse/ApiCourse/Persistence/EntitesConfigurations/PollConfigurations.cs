using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace ApiCourse.Persistence.EntitiesConfigurations
{
    public class PollConfigurations : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Title).IsUnique();
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Summary).HasMaxLength(1500);
        }
    }
}
