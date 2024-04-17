using Microsoft.EntityFrameworkCore;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 


        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();


            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ImageURL).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(12, 2);
            modelBuilder.Entity<Category>().HasMany(g => g.Products).WithOne(c => c.Category).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Material escolar"
                },
                new Category
                {
                    Id = 2,
                    Name = "Acessórios"
                }
           );

        }
    }
}
