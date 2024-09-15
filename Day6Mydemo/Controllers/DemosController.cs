using Microsoft.AspNetCore.Mvc;

namespace Day6Mydemo.Controllers
{
    public class DemosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SetTempData()
        {
            TempData.Add("name", "samrtsoftware");
            TempData["age"] = 25;
            return Content("saved tempdata........");
        }
        public IActionResult GetTempData()
        {
            string name = TempData["name"].ToString();
            int age = (int) TempData["age"];
            return Content($"Name: {name}, Age: {age}");
        }
        public IActionResult GetTempData2()
        {
            string name = TempData.Peek("name").ToString();
            int age = (int)TempData.Peek("age");
            return Content($"Name: {name}, Age: {age}");
        }
        public IActionResult GetTempData3()
        {
            string name = TempData["name"].ToString();
            int age = (int)TempData["age"];
            TempData.Keep();
            return Content($"Name: {name}, Age: {age}");
        }
        public IActionResult SetCookies()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = System.DateTime.Now.AddDays(15);
            Response.Cookies.Append("name", "smartsoftware", cookieOptions);
            Response.Cookies.Append("age", "25",cookieOptions);
            return Content("Cookies saved........");
        }

        public IActionResult GetCookies()
        {
            string name = Request.Cookies["name"];
            string age = Request.Cookies["age"];
            return Content($"Name: {name}, Age: {age}");
        }

        public IActionResult SetSessions()
        {

            HttpContext.Session.SetString("name", "smartsoftware");
            HttpContext.Session.SetInt32("age", 25);
            return Content("Session saved........");
        }

        public IActionResult GetSessions()
        {
            string name = HttpContext.Session.GetString("name");
            int? age = HttpContext.Session.GetInt32("age");
            return Content($"Name: {name}, Age: {age}");
        }
    }
}
