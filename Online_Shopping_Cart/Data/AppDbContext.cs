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
        //the mapping of the database relations and the objects in known as the enttiy framework. 
        //in database we have the realtional data and we call it the object relational database
        // These objects are the tables name that the database contains for the project (ORM object relational Modeling)
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Catogories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //modelBuilder.Entity<Product>().HasOne(m=>m.Category).WithMany().OnDelete(DeleteBehavior.NoAction);//one product have many categories
          //modelBuilder.Entity<Product>().HasOne(m=>m.Brand).WithMany().OnDelete(DeleteBehavior.NoAction);//one product have many categories
            base.OnModelCreating(modelBuilder);
        }


    }
}
