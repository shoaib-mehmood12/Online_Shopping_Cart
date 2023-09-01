using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Cart.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Slug { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

        public int Stock { get; set; }//when stock we get so we add in this and when the stock will less then we subtract

        public DateTime? ReleaseDate { get; set; }//when the product is release and ? mean it may be nullable(assign a null value to a variable if user not enter the value).
        public string Category { get; set; }//foreign-key of the category class becuse 1(category)->many(products) && many(products)->one(category) so by this forign key we will tell that from which category the product exist.

        public string ImageUrl { get; set; }// for database
    }
}
