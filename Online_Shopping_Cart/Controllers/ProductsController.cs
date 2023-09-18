using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Online_Shopping_Cart.Data;
using Online_Shopping_Cart.Handlers;
using Online_Shopping_Cart.Models;
using System.Security.Policy;

namespace Online_Shopping_Cart.Controllers
{
    //[Authorized(Roles =$"{GlobalsConfig.AdminRole},{GlobalsConfig.ShopkeeperRole}")]// Inside the brackets it will not accept the variables
    [Authorized]
    public class ProductsController : Controller
    {
       
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var product = _context.Products
    .Select(m => new ProductViewModel
    {
        Brand = m.Brand.Name,
        Name = m.Name,
        Category = m.Category.Name,
        Description = m.Description,
        Price = m.Price,
        Slug = m.Slug,
        ReleaseDate = m.ReleaseDate,
        Id = m.Id,
        Stock = m.Stock,
        ImageUrl = m.Images.OrderBy(n => n.DbEntryTime).Select(n => n.URL).FirstOrDefault(),
    });
            return View(product);
        }
        public IActionResult Create()
        {

            //ref: line# 07 category create
          if(!_context.Categories.Any(m => m.Status && m.Type == CategoryType.Category)) //if we are creating the product so this is mandatory that the we have the brand and category both will have exists with the active status.
            {
                TempData["Notification"] = "No category exist with the active status"; // to store the data temporary and when the request complete the data will dispose.
                return RedirectToAction("create","category");//if there no category whose type is category and status is active then redirect to action whose method is create and the model is category
            }
            if (!_context.Categories.Any(m => m.Status && m.Type == CategoryType.Brand)) //if we are creating the product and we have not created the category so this condition will move us to the create category page to create firstly category and then add the product.
            {
                TempData["Notification"] = "No Brand exist with the active status"; // to store the data temporary and when the request complete the data will dispose.
                return RedirectToAction("create", "category");//if there no category whose type is category and status is active then redirect to action whose method is create and the model is category
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
             if(ModelState.IsValid)
            {
                if (model.Uploads?.Any() == true)
                {
                    model.Images ??= new(); // model.Images ??= new List<productImage>
                    int imageRank = 0;
                            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                            string appPath = Path.Combine("images", "Products");
                            string directryPath = Path.Combine(basePath, appPath);
                            Directory.CreateDirectory(directryPath);
                    foreach (var item in model.Uploads)
                    {
                        if ( item.Length > 0)
                        {
                            string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(item.FileName);

                            using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);//image create in file
                            item.CopyTo(stream);
                            model.Images.Add(new ProductImage
                            {
                                Rank=++imageRank,
                                ProductId = model.Id,
                                URL = Path.Combine(appPath, fileName).Replace("\\", "/")
                            });
                            
                        }
                    }    
                }
                _context.Add(model);
                int r=_context.SaveChanges();
                TempData["Notification"] = "Product Created";
                return RedirectToAction(nameof(Index));
                
            }
            return View(model);
        }

        public IActionResult Edit(string id)
        {//var product = _context.Products.FirstOrDefault(p => p.Id == id);  //searching the product by taking the id 
            var product = _context.Products
               .Include(m => m.Images)
               .Include(m => m.Category)
               .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();//display 404 error that priduct not found
            }
            return View(product);// The upper product that we have taken through will return througn a bi
        }
        [HttpPost]
        public IActionResult Edit(Product model,List<string> deleteImagesIds)
        {
            if(ModelState.IsValid)
            {
                if (model.Uploads?.Any() == true)
                {
                    List<ProductImage> images = new(); // model.Images ??= new List<productImage>
                    int imageRank = 0;
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string appPath = Path.Combine("images", "Products");
                    string directryPath = Path.Combine(basePath, appPath);
                    Directory.CreateDirectory(directryPath);
                    foreach (var item in model.Uploads)
                    {
                        if (item.Length > 0)
                        {
                            string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(item.FileName);

                            using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);//image create in file
                            item.CopyTo(stream);
                            images.Add(new ProductImage
                            {
                                Rank = ++imageRank,
                                ProductId = model.Id,
                                URL = Path.Combine(appPath, fileName).Replace("\\", "/")
                            });

                        }
                    }                                                                                                                 
                    var imagesToDelete= _context.ProductImages.Where(m=>m.ProductId==model.Id && deleteImagesIds.Contains(m.Id)).ToList();//why we add product id---> for safety purpose in a way that user will only delete the imgaes that is belonging to to its product
                
                _context.AddRange(images);
                    var imagesUrlsToDelete = imagesToDelete.Select(m => m.URL).ToList();
                    _context.RemoveRange(imagesToDelete);//delete from database
                    _context.Update(model);
                int v = _context.SaveChanges();
                    if(v > 0)//Delete form directory
                    {
                        foreach (var url in imagesUrlsToDelete)
                        {
                            try
                            {
                                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url));
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                TempData["Edit"] ="Product Updated";
                return RedirectToAction(nameof(Index));

                }

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
            TempData["Del"] = "Product Deleted";
            return RedirectToAction(nameof(Index));

        }
    }
}
