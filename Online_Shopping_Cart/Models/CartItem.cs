using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Cart.Models
{
    public class CartItem: SharedModel
    {
        //object identify that which shopping cart/ basket
        [Required]
        public string ShoppingCartID { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        //object identify which product is inside the shop  ping cart
        [Required]
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }// quantity of the product  
    }
}
