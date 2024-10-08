﻿using System;
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
        public IHttpContextAccessor httpcontextAccessor { get; }

        //why private set because we want to set inside the calss means we are setting it in the below ProcessLogin method
        private  AppUserViewModel LoggedInUser { get;  set; } // we use this to store the user's data once. as we know that in single request the Logged in user is checked in multiple times so for each time the program will get the data from the database so for the same user go again and again is not a good practice so for this we use this object that will store the Logged in user util the session is on if session expires then the User also expires.  
        public AppDbContext (DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            httpcontextAccessor = contextAccessor;
          //      ProcessLogIn();//when this is called then at that time this method will get the user through the session.
        }                       
        //the mapping of the database relations and the objects in known as the enttiy framework. 
        //in database we have the realtional data and we call it the object relational database
        // These objects are the tables name that the database contains for the project (ORM object relational Modeling)
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<CartItem> CartItems { get; set; }                                                            
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<LoginHistory> LoginHistory { get; set; }

        public AppUserViewModel GetLoggedInUser()// this method will get the user from the database when i requires the loggined user
        {
            if(LoggedInUser != null)
            {
                return LoggedInUser;
            }
            // we are getting this from the session firslty then we use the Cookie.
            //var userId  =httpcontextAccessor.HttpContext.Session.GetString(GlobalsConfig.LoginSessionName);// user id against the session.
           var token= httpcontextAccessor.HttpContext.Request.Cookies[GlobalsConfig.LoginCookieName]?.toString();
            if(string.IsNullOrEmpty(token) ) // incase of mobile user we are not getting the token from the cookie we are getting this from the header.
            {
				  token = httpcontextAccessor.HttpContext.Request.Headers[GlobalsConfig.LoginCookieName].ToString();
			}
            
            if (!string.IsNullOrEmpty(token)) //browser user
            {
                var loginHistory = LoginHistory.Where(m=>m.Token==token).FirstOrDefault();
                if (loginHistory == null || loginHistory.ValidTill < DateTime.Now)
                {
					httpcontextAccessor.HttpContext.Response.Cookies.Delete(GlobalsConfig.LoginCookieName, new CookieOptions{
                      IsEssential=true, 
                    });
                    return null;
                }
           //i have to get the object of the user from the database.
            var user=LoginHistory.Where(m=>m.Token==token).Select(m=>m.User)//select the user through the Toekn
                .Select(m => new AppUserViewModel
                {
                    Name = m.Name, 
                    Id=m.Id,
                    Email = m.Email,
                    Roles=Roles.Select(n=>n.Name).ToList(),
                }).FirstOrDefault();// we Picked the user from the database
            LoggedInUser=user;
                return LoggedInUser;
            }
            else
            {
                return null;
            }
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //modelBuilder.Entity<Product>().HasOne(m=>m.Category).WithMany().OnDelete(DeleteBehavior.NoAction);//one product have many categories
          //modelBuilder.Entity<Product>().HasOne(m=>m.Brand).WithMany().OnDelete(DeleteBehavior.NoAction);//one product have many categories
            base.OnModelCreating(modelBuilder);
        }



    }
}
