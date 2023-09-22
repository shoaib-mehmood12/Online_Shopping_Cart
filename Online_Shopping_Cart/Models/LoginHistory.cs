namespace Online_Shopping_Cart.Models
{
	public class LoginHistory  :SharedModel
	{
		public string UserId { get; set; }
		public virtual AppUser User { get; set; }
		public string IpAddress { get;set; }
		public string ClientInfo { get; set; }
		public string OS { get; set; }
		public DateTime ValidTill { get; set; }
		public DateTime? ClosedOn { get; set; }

		public string Token { get; set; }= Guid.NewGuid().ToString().Replace("-","");
	}
}
