using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_Cart.Models
{
    public class Product:SharedModel
    {
        [Required(ErrorMessage = "The slug field is required ")]
        [StringLength(150)]
        [RegularExpression("^[a-z0-9-]{1,150}$", ErrorMessage = "The slug must have lower case Alpha-Numeric")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "The slug field is required ")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required ")]
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must greater than 0.01")]
        public decimal Price { get; set; }

        [ForeignKey("Brand")]
        public string BrandId { get; set; }
        public virtual Category Brand { get; set; }

        [Required(ErrorMessage = "The Stock Field is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non negative number")]
        public int Stock { get; set; }//when stock we get so we add in this and when the stock will less then we subtract

        [Required(ErrorMessage = "The release date is required")]
        public DateTime? ReleaseDate { get; set; }//when the product is release and ? mean it may be nullable(assign a null value to a variable if user not enter the value).

        [Required(ErrorMessage = "The category field is required")]
        [ForeignKey("Category")]
        public string CategoryId { get; set; }//foreign-key of the category class becuse 1(category)->many(products) && many(products)->one(category) so by this forign key we will tell that from which category the product exist.
        public virtual Category Category { get; set; }//creating the object of the category 
         
        public virtual List<CartItem> CartItems { get; set; }//Navigate that user only add the product not purchase

        //navigate that user purchase the product after carting
        public virtual List<OrderDetail> OrderDetails { get; set; }//it means that many(ProductDisplaySelection)->many(product) so for this we have the list in both in one another
    }
}
