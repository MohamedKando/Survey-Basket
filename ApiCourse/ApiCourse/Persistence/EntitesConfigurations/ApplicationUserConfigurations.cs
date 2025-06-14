using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace ApiCourse.Persistence.EntitiesConfigurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");
            builder.Property(x=>x.FirstName).HasMaxLength(30);
            builder.Property(x=>x.LastName).HasMaxLength(30); 
        }
    }
}
