using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Scripting;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Models;
using System.Security.Cryptography.Xml;

namespace Online_Shopping_Cart.Controllers
{
    public class CategoriesController : Controller
    {
        //intializing the database we can use to store in data base by the simply using the word "context"
       //injecting the database into the controller.
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;   
        }

        //this is the interface this each interface is connect to a specific view so that each interface will give the view according to the connection of view inside itself
        //
        public IActionResult Index(string k, CategoryType? type)
        {
            if (type == null)
            {
                type = CategoryType.Category;
            }
                ViewBag.Type = type;// we are also storing this type in the type view bags

            var categoryQuery = _context.Categories.Where(m => m.Type == type);//we add here asQueryable In a way to Apply the queries so through the query we will find the string that user gives us.
            if (!string.IsNullOrWhiteSpace(k))//if the string is not null(k) name of the string is K that we are geting from the user from the index view
            {
                categoryQuery = categoryQuery.Where(x => x.Name.Contains(k));
            }
            //we are defining the viewbag that in this table search and giving it key to search in the given table.
            ViewBag.SearchUrl = "/Categories";// also add this corresponding in the admin layout
                                              // ViewBag.SearchKeyword = k;  
            var data = categoryQuery.Select(m => new CategoryViewModel { 
                     BrandWiseProducts=m.BrandWiseProducts.Count(),
                     CategoryWiseProducts=m.CategoryWiseProducts.Count(),
                     Name=m.Name,
                     Id=m.Id,
                     Type=m.Type,
                     LogoUrl=m.LogoUrl,
                     Status=m.Status
                  
            }).ToList();//the data we found above we added that data to list so we can 

            return View(data);


            //this whole code inside the index interface is to search inisde the category.
            // var categoryQuery = _context.Categories.Where(m=>m.Type==type);//we add here asQueryable In a way to Apply the queries so through the query we will find the string that user gives us.
            // if (!string.IsNullOrWhiteSpace(k ))//if the string is not null(k) name of the string is K that we are geting from the user from the index view
            // {
            //     categoryQuery = categoryQuery.Where(x => x.Name.Contains(k));
            // }
            // //we are defining the viewbag that in this table search and giving it key to search in the given table.
            // ViewBag.SearchUrl = "/Categories";// also add this corresponding in the admin layout
            //// ViewBag.SearchKeyword = k;  
            // var data = categoryQuery.ToList();//the data we found above we added that data to list so we can 
            // return View(data); //displays the list of the data in the index vew that we have found above.
        }
        public IActionResult Create(bool iar)
        {
            if (iar)// in the index view when the inline crate is call so at the index ajax we have set the iar true so through this that will true this condition and send the partial view of the create in the index page
            {
                Thread.Sleep(500); // for loader/spinner view
                return PartialView();//returns the partialview of create (displays a create view to get the data from user at the same index page without page reaload)
            }
            else// if we not approacing the index inline view so we will send the normal create-page view  
            {
                return View();// normal view of the create
            }
        }

        [HttpPost]
        public IActionResult Create(Category model )
        {
            string ca = 0.ToString();
            
            if(ModelState.IsValid)//check that whether the state of the model is valid or not( means have  the model contain the invalid or not if not then condition becomes true 
            {
                //firstly we will get the logo image by the user and then we are storing the logo's URL in the database not actual image of the logo. 
                // for this we are getting the Logo image from and then we storing the URL of that image only not the actual image of the logo. 
                if(model.Logo!=null && model.Logo.Length > 0)// to store the image in the folder.
                {
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");// this Line combines the current working directory of the application with the "wwwroot" directory to create a full path to the "wwwroot" directory relative to where your application is running.
                    string appPath = Path.Combine("images", "categories");// this line will combine the images and categories folder's Path and then store path into the appPath.
                    string directryPath=Path.Combine(basePath, appPath);// this will combine the both basePath and appPath and then store thier path in the directoryPath.
                    Directory.CreateDirectory(directryPath);// this line is creating folder(directory)  inside the wwwroot--> images-->>categories
                   // Encrypt the name of the file in a way to get rid of duplicate image overridden
                    string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.Logo.FileName);
                    
                    using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);//image create/paste in a directory-->> we are joining the file name with the directrypath and then we are storing the whole path of the image to the var stream
                    model.Logo.CopyTo(stream);//copying the model.logo to the stream // model is an Object and the logo is its property. // we are desposing the previous name of the image.
                      
                    model.LogoUrl =Path.Combine(appPath,fileName).Replace("\\","/");
                }
                //_context.Category.Add(model).
                //why we add the model instead of directly saving the changes?
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

                    using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);//creating the file && Opening the file because the stream is used to read and write the data so we are using the stream to firstly open the file and then we are using the same stream below the line and pasted the logo model to the file.
                    model.Logo.CopyTo(stream);
                    model.LogoUrl = Path.Combine(appPath, fileName).Replace("\\", "/");// Pasting Photo inthe file
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
           
            return View();

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

            var category = _context.Categories.Find(id);// we are storing the detail of the category from the category table whose id matches to the variable named as the category.
            if(category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Delete"] = "Item Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
