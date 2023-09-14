using Microsoft.AspNetCore.Mvc;


namespace Online_Shopping_Cart.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Vue()
        {
            return View();
        }
        public IActionResult Vue2()
        {
            return View();
        }
        public IActionResult OneTech()
        {
            return View();
        }
        public IActionResult Pullux()   
        {
            return View();
        }
		public IActionResult ThemeOneTech()
		{
			return View();
		}
		public IActionResult ThemePullux()
		{
			return View();
		}//this is test comment
         //added
        public IActionResult ExtensionMethods()
        {
            string obj = "hello this is shoaib";
            //To convert the following string into the tittle case We can use the following 3 lines but these are very lengthy so for this we will use the extension method(custom build).
            //obj=string.Join(" ",obj.Split(' ').Select(m=>m.Substring(0,1).ToUpper() + m.Substring(1).ToLower()));    
            //obj=string.Join(" ",obj.Split(' ').Select(m=>m[..1].ToUpper() + m[1..].ToLower()));    
            //CultureInfo.CurrentCulture.TextInfo.ToTitleCase(obj);

            //extension Method
            obj.ToTittleCase();// convert the string into the the title case 
            obj.CountAlfaNumeric();//count the alphabets and numbers.
            obj.ToSlug();//we have created a Extension method for the(automated convert the simple text into  Slugs format)
            obj.toString();// convert the data type into the string
            obj.WordCount('-');      
            return View();
        }

    }
}
