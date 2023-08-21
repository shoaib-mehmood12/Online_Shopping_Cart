using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_Cart.Models
{
    public class ShoppingCart
    {
        [Key]
        public string Id { get; set; }

        //foreign key User & the object of User --->> to identify which user have basket.
        [Required]
        [ForeignKey("User")]
        public string userId { get; set; }
        public AppUser User { get; set; }
        
        public List<CartItem> CartItems { get; set; }//contain the items/product that user want to buy
    }
}
