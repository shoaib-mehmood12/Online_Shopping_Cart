using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Models;

namespace Online_Shopping_Cart.Controllers
{
    public class CategoryController : Controller
    {
        //intializing the database we can use to store in data base by the simply using the word "context"
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;   
        }


        public IActionResult Index()
        {
            var data = _context.Catogories.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Category model )
        {
            if(ModelState.IsValid)//check that whether the state of the model is valid or not( means have  the model contain the invalid or not if not then condition becomes true 
            {
                //_context.Category.Add(model).
                _context.Add(model);//the database note the attributes(values) of the  category model
                _context.SaveChanges();//the the database will add the values of the category to the Actuall table
                //form one action to another action
                return RedirectToAction(nameof(Index));
            }
            // ModelState.AddModelError("Name", "Custom error");
            return View(model);
        }
        public IActionResult Edit(string id)
        {
            var category=_context.Catogories.Find(id);
            if (category == null) return NotFound();
          
            return View(category);
           
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);//update the changes that we have done in the above get method of the Edit
                _context.SaveChanges();//save the changes in the database
                return RedirectToAction(nameof(Index));
            }
           
            return View(model);

        }
        public IActionResult Delete(string id)
        {
            var category = _context.Catogories.Find(id);
            if (category == null) return NotFound();

            return View(category);

        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string id)
        {
            var category = _context.Catogories.Find(id);
            if(category == null) return NotFound();
            _context.Catogories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        

    }
}
