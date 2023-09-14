using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_Cart.Models
{
    public class Category:SharedModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        [Remote("CategoryNameCheck","RemoteValidations",AdditionalFields ="Id",ErrorMessage="Name already exists")]// this is used for check that whether this name already exists or not in the database if exist then send the error and not enter the value.
        public string Name { get; set; }//name of the category-->>e.g samsung    

        [Required(ErrorMessage = "Description Required")]
        [StringLength(100)]
        public string Description { get; set; }//description of the category

        public int Rank { get; set; }//rank allot to the category

        ////public CategoryStatus Status { get; set; }// the status of the category is alloted from the enum (active or not)
        //[Required(ErrorMessage = "Image url is required")]
        //public string ImageUrl { get; set; }//this will use in the view where we will be add the images of the product.
       
        [NotMapped]
        public IFormFile Logo { get; set; }
        
        [StringLength(200)]
        public string LogoUrl { get; set; }//this will use in the view where we will be add the logo of the product.
       

        public bool Status { get; set; }

        //one(category) have many(product)
        [InverseProperty("Brand")]
        public virtual List<Product> BrandWiseProducts { get; set; }//one category have more than on products 1(category)--many(products) thats why we added the list of the products in the category[e.g]--> samsung is a category and is have more than one product.
        [InverseProperty("Category")]
        public virtual List<Product> CategoryWiseProducts { get; set; }//one category have more than on products 1(category)--many(products) thats why we added the list of the products in the category[e.g]--> samsung is a category and is have more than one product.
        
        public CategoryType Type { get; set; }
    }
    public enum CategoryType{
        Category = 0,
        Brand = 10,
        Blogs = 20

    }
}

