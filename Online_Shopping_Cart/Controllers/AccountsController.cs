using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Handlers;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()// for the information of the user to understand that how many and which kinds of students are involved.
        {
              return View(await _context.Users.ToListAsync());
        }

        //// GET: Accounts/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var appUser = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(appUser);
        //}

        // GET: Accounts/Create


        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Password,ConfirmPassword,shoppingCartId")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                appUser.EncryptedPassword = (appUser.Id + appUser.Password).Encrypt();
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }
		public IActionResult ChangePassword()
		{
			return View();
		}


		[Authorized]
        public IActionResult ChangePassword(AppUser model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                var userId=  _context.GetLoggedInUser().Id;// we picked the id of the user only from the logged in table 
                var user= _context.Users.Where(m=>m.Id==userId).FirstOrDefault();// now we are getting the whole User from the database to Update the user Password
                user.EncryptedPassword = (userId + model.Password).Encrypt();
                _context.SaveChanges();
                _context.httpcontextAccessor.HttpContext.Response.Cookies.Delete(GlobalsConfig.LoginCookieName, new(){
                    IsEssential=true,   
                });
                return RedirectToAction("index","Login");
            }
            ModelState.AddModelError("", "Both password not Found"); //The first parameter, "", is typically used to specify the field name associated with the error. In this case, an empty string "" is used, which implies that the error is not specific to a particular field but is a general error message. You might use a field name here if you want to associate the error with a specific form field.
			return View(model);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Email,Password,ConfirmPassword,shoppingCartId")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appUser.EncryptedPassword=(appUser.Id + appUser.Password).Encrypt();
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var appUser = await _context.Users.FindAsync(id);
            if (appUser != null)
            {
                _context.Users.Remove(appUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(string id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}
