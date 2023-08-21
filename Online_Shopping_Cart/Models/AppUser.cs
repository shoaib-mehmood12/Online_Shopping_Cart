using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_Cart.Models
{
    public class AppUser:SharedModel
    {
        [Required(ErrorMessage ="Enter Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The EncyptedPassword field is required")]
        public string EncryptedPassword { get; set; }

        public List<AppRole> Roles { get; set; }
        
        public string shoppingCartId { get; set; } 
        public ShoppingCart ShoppingCart { get; set; }
    }
}
