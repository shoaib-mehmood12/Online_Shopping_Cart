using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
    public class AppRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AppRole model)
        {
            if (ModelState.IsValid)
            {
                
    }
            return View(model);
        }
    }
}
