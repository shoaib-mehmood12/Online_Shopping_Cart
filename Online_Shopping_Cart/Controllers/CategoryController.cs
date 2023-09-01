using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Scripting;
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


        public IActionResult Index(string k)
        {
            var categoryQuery = _context.Categories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(k))
            {
                categoryQuery= categoryQuery.Where(x => x.Name.StartsWith( k) ||  x.Description.Contains(k));
            }
            ViewBag.SearchUrl = "/Category";
            ViewBag.SearchKeyword = k;  
            var data = categoryQuery.ToList();
            return View(data); 
        }
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Category model )
        {
            string ca = 0.ToString();
            
            if(ModelState.IsValid)//check that whether the state of the model is valid or not( means have  the model contain the invalid or not if not then condition becomes true 
            {
                if(model.Logo!=null && model.Logo.Length > 0)
                {
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string appPath = Path.Combine("images", "categories");
                    string directryPath=Path.Combine(basePath, appPath);
                    Directory.CreateDirectory(directryPath);
                    string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.Logo.FileName);
                    
                    using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);//image create in file
                        model.Logo.CopyTo(stream);
                    model.LogoUrl =Path.Combine(appPath,fileName).Replace("\\","/");
                }
                //_context.Category.Add(model).
                _context.Add(model);//the database note the attributes(values) of the  category model
                _context.SaveChanges();//the the database will add the values of the category to the Actuall table
                                       //form one action to another action
                if (model.Type == 0)
                {
                     ca = "Category";

                }
                else
                {
                    ca = "Brand";
                }

                TempData["Notification"] = ca.ToString(); 
                //TempData["Notification"] = " Category Created"; 
                return RedirectToAction(nameof(Index));
            }
            // ModelState.AddModelError("Name", "Custom error");
            return View(model);
        }
        public IActionResult Edit(string id)
        {
            var category=_context.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
           
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            string ca =0.ToString();
            if (ModelState.IsValid)
            {
                if (model.Logo != null && model.Logo.Length > 0)
                {
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string appPath = Path.Combine("images", "categories");
                    string directryPath = Path.Combine(basePath, appPath);
                    Directory.CreateDirectory(directryPath);
                    string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.Logo.FileName);

                    using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);//image create in file
                    model.Logo.CopyTo(stream);
                    model.LogoUrl = Path.Combine(appPath, fileName).Replace("\\", "/");
                }


                _context.Update(model);//update the changes that we have done in the above get method of the Edit
                _context.SaveChanges();//save the changes in the database
                if (model.Type == 0)
                {
                    ca = "Category";

                }
                else
                {
                    ca = "Brand";
                }
                TempData["Edit"] =ca.ToString() ;
                return RedirectToAction(nameof(Index));
            }
           
            return View(model);

        }
        public IActionResult Delete(string id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            return View(category);

        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(string id)
        {

            var category = _context.Categories.Find(id);
            if(category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Delete"] = "Item Deleted";
            return RedirectToAction(nameof(Index));
        }

         

    }
}
