using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Cart.Models;
using System.Diagnostics;

namespace Online_Shopping_Cart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {                
            _logger = logger;
        }
         
        public IActionResult Index()
         {
            // we did this because when the project starts so at that time our view before start check that if user is entered or not, if use not entered so its means that the username in the session is blank so on that basis we have move to the Log in page
            
              // comments the two lines below because we not want to show the login inputs on the index screev first 
            //var userName=HttpContext.Session.GetString("UN");
            //if (string.IsNullOrEmpty(userName)) { return RedirectToAction("Index", "Login"); }
             return View(); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}