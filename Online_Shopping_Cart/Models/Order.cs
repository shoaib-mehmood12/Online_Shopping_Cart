using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Cart.Models
{
    public class Order:SharedModel
    {
        //Identify who is the user who orders
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        //what is the date when the Orderplaced
        [Required]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "The Shipping Address is required")]
        [StringLength(500)]
        public string ShippingAddress { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }

        public List<OrderDetail> Details { get; set; }
    }
}
