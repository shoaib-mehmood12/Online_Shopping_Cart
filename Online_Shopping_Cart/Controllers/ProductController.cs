using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
    public class ProductController : Controller
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
        public IActionResult Create(Product model)
        {
            if(ModelState.IsValid)
            {

                var x = 1;
            }
            return View(model);
        }
    }
}
