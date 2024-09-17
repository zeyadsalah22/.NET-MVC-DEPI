using Microsoft.AspNetCore.Mvc;

namespace Day6Mydemo.Controllers
{
    public class TestTwoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
