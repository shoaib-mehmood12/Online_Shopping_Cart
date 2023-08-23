using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Products.ToList();
            return View(data);
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
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
                
            }
            return View(model);
        }
        public IActionResult Edit(string id)
        {
            var product = _context.Products.Find(id);
            if(product == null) { }
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if(ModelState.IsValid)
            {
                _context.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
          return View();
        }
        public IActionResult Delete(string id)
        {
            var product = _context.Products.Find(id);
            if (product == null) { }
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            _context.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
