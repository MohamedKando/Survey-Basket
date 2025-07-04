using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebApplication4.Areas.Employees.Models;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options)
        {
                
        }
        public DbSet<Item> items { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Employees> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>()
                .Property(e => e.DateTime)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Category>().HasData(new Category() { Id = 1, Name = "Select Category" },
                new Category() { Id = 2, Name = "Computer" },
                new Category() { Id = 3, Name = "Labtop" },
                new Category() { Id = 4, Name = "Mobile" });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name="Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER"
                }) ;
          
        }
    }
    
}
 