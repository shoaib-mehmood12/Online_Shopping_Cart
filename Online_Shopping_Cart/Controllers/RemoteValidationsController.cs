using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Migrations;
using Online_Shopping_Cart.Models;
using Category = Online_Shopping_Cart.Models.Category;

namespace Online_Shopping_Cart.Controllers
{
    public class RemoteValidationsController : Controller
    {
        private readonly  AppDbContext _context;
        public RemoteValidationsController(AppDbContext context)
        {
            _context = context;
        }
        //public IActionResult CategoryNameCheck(Category category)
        //{
        //    var isExisting= _context.Categories.Where(m => m.Id!=category.Id && m.Name.Trim() == category.Name.Trim()).Any();
        //    return Json(!isExisting);// if name exist then sent false  
        //}

        public IActionResult CategoryNameCheck(Category category)
        {
           if (!string.IsNullOrEmpty(category.Id))
            {
                var result=_context.Categories.Any(m => m.Id!=category.Id && m.Name==category.Name);
                return Json(!result);
            
            }
            else 
            {
                var names = _context.Categories.Select(m => m.Name).ToList();// we picked the names of all the categories in list
                names.ForEach(m => m = string.Join("", m.Where(c => char.IsLetterOrDigit(c))));// we picked up all the categories and remove the spaces and whitespaces, and specialCharacters.
                category.Name = string.Join("", category.Name.Where(c => char.IsLetterOrDigit(c)));  //we picked all the names of the names of the categories and remove the spaces and whitespaces, and specialCharacters. 
                var result = names.Any(m => m.ToLower() == category.Name.ToLower());

                return Json(!result);// if name exist then sent false 
            }
             
        }
        public IActionResult AppUserEmailCheck(AppUser appUser)
        {
                var isExisting = _context.Users.Where(m => m.Email == appUser.Email).Any();
                    return Json(!isExisting);
                
            
        }
    }
}
