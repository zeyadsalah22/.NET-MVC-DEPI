using Microsoft.AspNetCore.Mvc;

namespace Day6Mydemo.Controllers
{
    public class TestOneController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("session1", "welcome in My site Until Brower Closes");
            // send data Between
            TempData["FullRequest"] = "TempData";
            ViewData["ViewDataVal"] = "View Data ";
            ViewBag.viewbagval = "View bag ";
            // return View();

            return RedirectToAction("Show");
        }

        public IActionResult Show()
        {
            ViewData["ViewDataVal"] = "From the Show Action";
            ViewBag.viewbagval = "From the Show Action ";
            return View();
        }
    }
}
