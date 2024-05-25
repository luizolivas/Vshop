using Microsoft.EntityFrameworkCore;
using Vshop.CartApi.Models;

namespace Vshop.CartApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product>? Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
           .HasKey(c => c.Id);

            //Product
            modelBuilder.Entity<Product>().
               Property(c => c.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Product>().
               Property(c => c.Name).
                 HasMaxLength(100).
                   IsRequired();

            modelBuilder.Entity<Product>().
              Property(c => c.Description).
                   HasMaxLength(255).
                       IsRequired();

            modelBuilder.Entity<Product>().
              Property(c => c.ImageURL).
                  HasMaxLength(255).
                      IsRequired();

            modelBuilder.Entity<Product>().
               Property(c => c.CategoryName).
                   HasMaxLength(100).
                    IsRequired();

            modelBuilder.Entity<Product>().
               Property(c => c.Price).
                 HasPrecision(12, 2);

            //CartHeader
            modelBuilder.Entity<CartHeader>().
                 Property(c => c.UserId).
                 HasMaxLength(255).
                     IsRequired();

            modelBuilder.Entity<CartHeader>().
               Property(c => c.CouponCode).
                  HasMaxLength(100);
        }

    }
}
