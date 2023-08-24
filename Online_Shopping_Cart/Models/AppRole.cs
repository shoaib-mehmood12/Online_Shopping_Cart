using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_Cart.Models
{
    public class AppRole: SharedModel
    {
        [Required(ErrorMessage = "Name field is required ")]
        [StringLength(50)]
        public string Name { get; set; }

        public List<AppUser> User { get; set; }

   
    }
}
