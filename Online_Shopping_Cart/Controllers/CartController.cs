using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Handlers;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
	[Authorized]
	public class CartController : Controller
	{
		private AppDbContext _context;
		private string loggedInUserId;

		public CartController(AppDbContext context)
		{
			_context = context;
			loggedInUserId = _context.GetLoggedInUser()?.Id; // to get teh loggedin user from the db 
		}
		[Authorized]// if user not loggedin then user not enter to the cart page 
		  public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddOrUpdateCart(string id,int qty=1, bool isUpdate=false)// id of the product which we have to adding in the cart
		{
			System.Threading.Thread.Sleep(250);
			var product = _context.Products.FirstOrDefault(p=>p.Id==id); // finding the product from the db by using the id that we have recieved in the above parameters
			 if(product == null)//if the product not found in teh database then return the message to user not found the product
			{
				return Json(new {Status=false,Msg="product not found for id"+id});
			}
			 var cart=_context.Carts.Include(m=>m.CartItems).Where(m=>m.UserId==loggedInUserId).FirstOrDefault();//picking the cart of the Loggedinuser.
			if (cart == null)  // if loggedin user have no cart so we alot him a cart
			{
				cart = new ShoppingCart {UserId=loggedInUserId,CartItems=new() }; // allotiong the cart	on runtime
				_context.Carts.Add(cart);
				_context.SaveChanges();
			}
				var existingItem=cart.CartItems.Where(m=>m.ProductId==id).FirstOrDefault() ;
			if(existingItem!=null )// if we have product inside the cartitem then we only increment in the existing products.
			{
				if (isUpdate)
				{
					existingItem.Quantity =qty;

				}
				else
				{
					existingItem.Quantity+=qty;
				}
				_context.SaveChanges();
			}
			else  //if the item not exist inside the cartitem
			{
				CartItem currentItem= new() { ProductId = id, Quantity = qty, ShoppingCartID = cart.Id };
				cart.CartItems.Add(currentItem);
				_context.Add(currentItem);
				_context.SaveChanges();
			}
			//till above we have done the work to add the product in teh cart and change the quantity
			// now we are preparing the data to display the user

			//var d = _context.Carts.Where(m => m.UserId == loggedInUserId).SelectMany(m => m.CartItems.Select(i => i.Product))
			//    .Select(m => new { })
			//    .ToList();
			//        or
			var productIds = cart.CartItems.Select(m=>m.ProductId).ToList();//picking all the ids of the products that are existing inside the carts.
		var products=_context.Products.Where(m=>productIds.Contains(m.Id)).Select(m => new
		{
			m.Id,
			m.Name,
			ImageUrl = m.Images.OrderBy(m => m.Rank).Select(m => m.URL).FirstOrDefault(),
			m.Price,
			categoryName = m.Category.Name,
			brandName = m.Brand.Name
		}).ToList(); ;// picking the products against the ids that we have picked above we use this method because through this we get only product that were added in the cart 	
			var result =products.Select(m => new
			{
				ProductId=m.Id,
				m.Name,
				m.categoryName,
				m.Price,
				m.ImageUrl,
				qty = cart.CartItems.Where(i => i.ProductId == m.Id).Select(q => q.Quantity).FirstOrDefault()//m is behaving like product
			}).ToList();
			return Json(new { Status = true, Data = result });
		}

		
			public IActionResult GetCartItems()// we are not getting something to user we are giving the data to the user
		{
			System.Threading.Thread.Sleep(250);
			if (string.IsNullOrEmpty(loggedInUserId))
			{
				return Json(new { Status = false, Msg = "Login Required" });
			}
			var cart = _context.Carts.Include(m => m.CartItems).Where(m => m.UserId == loggedInUserId).FirstOrDefault();//picking the cart of the Loggedinuser.
			if (cart == null)  // if loggedin user have no cart so we alot him a cart
			{
				cart = new ShoppingCart { UserId = loggedInUserId, CartItems = new() }; // allotiong the cart	on runtime
				_context.Carts.Add(cart);
				_context.SaveChanges();
			}
			
			var productIds = cart.CartItems.Select(m => m.ProductId).ToList();//picking all the ids of the products that are existing inside the carts.
			var products = _context.Products.Where(m => productIds.Contains(m.Id)).Select(m => new
			{
				productId=m.Id,
				m.Name,
				ImageUrl = m.Images.OrderBy(m => m.Rank).Select(m => m.URL).FirstOrDefault(),
				m.Price,
				categoryName = m.Category.Name,
				brandName = m.Brand.Name
			}).ToList(); ;// picking the products against the ids that we have picked above we use this method because through this we get only product that were added in the cart 	
			var result = products.Select(m => new
			{
				m.productId,
				m.Name,
				m.categoryName,
				m.Price,
				m.ImageUrl,
				qty = cart.CartItems.Where(i => i.ProductId == m.productId).Select(q => q.Quantity).FirstOrDefault()//m is behaving like product
			}).ToList();
			return Json(new { Status = true, Data = result });
		}
		public IActionResult DeleteItem(string id) {
			var product = _context.Products.FirstOrDefault(p => p.Id == id); // finding the product from the db by using the id that we have recieved in the above parameters
			var cart = _context.Carts.Include(m => m.CartItems).Where(m => m.UserId == loggedInUserId).FirstOrDefault();//picking the cart of the Loggedinuser.
			var existingItem = cart.CartItems.Where(m => m.ProductId == id).FirstOrDefault();
			 if(existingItem != null)
			{
				_context.Remove(existingItem);
			}
			 _context.SaveChanges();
			return GetCartItems();
		}

	}
}
