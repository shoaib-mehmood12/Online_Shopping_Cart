using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Catogories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //modelBuilder.Entity<Product>().HasOne(m=>m.Category).WithMany().OnDelete(DeleteBehavior.NoAction);//one product have many categories
          //modelBuilder.Entity<Product>().HasOne(m=>m.Brand).WithMany().OnDelete(DeleteBehavior.NoAction);//one product have many categories
            base.OnModelCreating(modelBuilder);
        }


    }
}
