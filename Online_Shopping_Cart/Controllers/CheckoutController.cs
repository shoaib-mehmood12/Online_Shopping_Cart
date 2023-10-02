using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Migrations;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
	public class CheckoutController : Controller
	{
		public AppDbContext _context;

        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()  //get of the checkout
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(Order model)  //Post of the checkout
		{    var userId=_context.GetLoggedInUser().Id;
            model.UserId = userId;
			var existingCart=_context.Carts.Where(m=>m.UserId==userId).Include(m=>m.CartItems).FirstOrDefault();
			model.Details = existingCart.CartItems.Select(m => new OrderDetail {
				ProductId = m.ProductId,
				QuantityDemanded = m.Quantity,
					
			}).ToList();//picking the cart of the Loggedinuser.
       		  model.Details.ForEach(m=>m.OrderId=model.Id);
			_context.Add(model);
			_context.RemoveRange(existingCart.CartItems);
			_context.Remove(existingCart);
			_context.SaveChanges();
			return RedirectToAction("Index", "Home");
		}
	}
}
