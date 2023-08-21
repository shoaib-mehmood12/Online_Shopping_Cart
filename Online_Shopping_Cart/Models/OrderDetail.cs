using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Cart.Models
{
    public class OrderDetail:SharedModel
    {
        //which order is this this foreign key id we identify the order
        [Required(ErrorMessage = "Enter OrderId")]
        public string OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public int QuantityDemanded { get; set; }
        public int? QuantitySent { get; set; }
    }
}
