using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ApiCourse.Persistence.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ApiCourse.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options , IHttpContextAccessor httpContextAccessor) : base(options )
        {
            _httpContextAccessor = httpContextAccessor;   
        }
        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Every Assembly i used to make the class is easly used across all project zy el fluentvalidation and maspter
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries =ChangeTracker.Entries<AuditableEntity>();

            foreach (var entryEntity in entries)
            {
                var currentUserId= _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (entryEntity.State == EntityState.Added)
                {
                    entryEntity.Property(x => x.CreatedById).CurrentValue = currentUserId!;
                }
                else if (entryEntity.State == EntityState.Modified)
                {
                    entryEntity.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                    entryEntity.Property(x => x.UpdatedOn).CurrentValue=DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
