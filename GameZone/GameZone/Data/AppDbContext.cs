using GameZone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameDevice>().HasKey(e => new { e.GameId, e.DeviceId });

            modelBuilder.Entity<Device>().HasData(
                new Device { Id=1,Name="Play Station", icon="bi bi-playstation" },
                new Device { Id = 2, Name = "Xbox", icon = "bi bi-xbox" },
                new Device { Id = 3, Name = "Personal Computer", icon = "bi bi-pc-display" },
                new Device { Id = 4, Name = "Nintendo Switch", icon = "bi bi-nintendo-switch" }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Story" },
                
                new Category { Id = 2, Name = "RGP" },
                new Category { Id = 4, Name = "Moba" },
                new Category { Id = 5, Name = "Shooting" },
                new Category { Id = 6, Name = "Stratgy" },
                new Category { Id = 7, Name = "Sport" }
                );
            modelBuilder.Entity<IdentityRole>().HasData(
                     new IdentityRole()
                     {
                         Id = Guid.NewGuid().ToString(),
                         Name = "Admin",
                         NormalizedName = "ADMIN"
                     },
                     new IdentityRole()
                     {
                         Id = Guid.NewGuid().ToString(),
                         Name = "User",
                         NormalizedName = "USER"
                     });

        }
    }
}
