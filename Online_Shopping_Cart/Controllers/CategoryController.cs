using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
    public class CategoryController : Controller
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
        public IActionResult Create(Category model )
        {
            if(ModelState.IsValid)
            {
                var x = 01;
            }
            // ModelState.AddModelError("Name", "Custom error");
            return View(model);
        }
        public IActionResult Edit()
        {
            return View();
        } 
        public IActionResult Delete()
        {
            return View();
        }

    }
}
