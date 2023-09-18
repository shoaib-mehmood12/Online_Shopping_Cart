namespace Online_Shopping_Cart
{
	public class GlobalsConfig
	{
        // In this we create only that variables which we use again and again 
        public static string LoginSessionName { get; } = "OSC-Session";
        // don't use the space in the names admin and shopkeeper.
        public const string AdminRole = "Admin";
        public const string ShopkeeperRole = "ShopKeeper";
        
    }
}
