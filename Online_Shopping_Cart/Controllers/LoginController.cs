using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Handlers;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{  
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() //when we press the Login button on the client side this w'll call.
        {
            return View();
        }
        // the password and the login 
        [HttpPost]
        public IActionResult Index(LoginViewModel model) //these parameters are comming from view
        {
          var user= _context.Users.Where(m => m.Email == model.Email).FirstOrDefault();//we use username as email
            if (user == null) //if password match then session on becuase user is valid and can login to the website
            {
                ModelState.AddModelError("Email","Email not Found");
                return View(model);
            }
            if(( user.Id + model.Password).Encrypt()==user.EncryptedPassword)
            {     //we are storing the Id(primary key) in the session to identify the sesssion
                HttpContext.Session.SetString(GlobalsConfig.LoginSessionName,user.Id);//on the sesssion on the email of the user whose password matches     
                return RedirectToAction("Index", "Home");//index of HomeController 
            }
            ModelState.AddModelError("Password", "Invalid password");
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(string username, string password) //these parameters are comming from view
        //{
        //    _context.Users.Where(m => m.Email == username);//we use username as email
        //    if () //if password match then session on becuase user is valid and can login to the website
        //    {
        //        HttpContext.Session.SetString("UN", username);//on the sesssion with the  username    
        //        return RedirectToAction("Index", "Home");//index of HomeController 
        //    }
        //    return View();
        //}



        public IActionResult Logout()
		{
			HttpContext.Session.Remove("UN");
			return RedirectToAction("Index", "Home");//index of HomeController
			
		}
	}
}
