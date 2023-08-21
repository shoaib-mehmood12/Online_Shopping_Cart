using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Online_Shopping_Cart.Models
{
    public class SharedModel
    {
        public SharedModel()
        {
               Id= Path.GetRandomFileName().Replace(".","");
                DbEntryTime = DateTime.UtcNow;
        }

        [ScaffoldColumn(false)]
        public string Id { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DbEntryTime { get; set; }
        [ScaffoldColumn(false)]
        public DateTime LastModified { get; set; }



    }
}
