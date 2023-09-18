
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Online_Shopping_Cart.Data;

namespace Online_Shopping_Cart.Handlers
{
	public class Authorized : ActionFilterAttribute, IAuthorizationFilter
	{
		public string Roles { get; set; }
		private List<string> RolesList=new();
		
		private bool IsAuthenticated { get; set; } = false;// purpose of this is : is the user is authenticted or not.
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			RolesList=(Roles ?? "").Split(new char[] {',',' '},StringSplitOptions.RemoveEmptyEntries).ToList();
			// we are not getting the session here because now we are getting  the user from the LoggedInUser. for this i have to inject the db context	
			// when the call comes then at that time the services also add(scope/singelton/transit)
			var db=(AppDbContext)  context.HttpContext.RequestServices.GetService(typeof(AppDbContext));
			var loggedInUser=db.GetLoggedInUser();
			if(loggedInUser != null)// if LoggedInUser is not null and empty means that we have to check the user role and then move to autherize
			{
				if(RolesList.Any()) 
				{ 
					//foreach(var requiredRole in RolesList)
					//{
					//	foreach (var userRole in loggedInUser.Roles)
					//	{
					//		if (requiredRole == userRole)
					//		{
					//			IsAuthenticated = true;
					//		}
					//		if (IsAuthenticated) break;
					//	}
					//	if(IsAuthenticated) break;
					//}
					IsAuthenticated = RolesList.Any(rr=>loggedInUser.Roles.Any(ur=>ur==rr));
				}
				else
				{
					IsAuthenticated = true;
				}
				
				//check  account status
				// other check
			 }
			if(!IsAuthenticated)// if user is not authencated from above conditions then 
			{

			context.Result = new RedirectResult("~/Login");
			}
		}
	}
}
