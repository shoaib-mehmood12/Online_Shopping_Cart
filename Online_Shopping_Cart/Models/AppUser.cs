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

        
        public string EncryptedPassword { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare("Password")]
        [Required(ErrorMessage = "Confirm Password field is required")]
        [NotMapped]
        public string  ConfirmPassword { get; set; }

        public List<AppRole> Roles { get; set; }
        
        public string shoppingCartId { get; set; } 
        public ShoppingCart ShoppingCart { get; set; }
    }

	public class AppUserViewModel : SharedModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public List<string> Roles { get; set; }
	}



    public class LoginViewModel {
        [Required(ErrorMessage ="Email Invalid")]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }   
        public bool RememberMe { get; set; }
       
    }

}
