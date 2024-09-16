using Day6Mydemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day6Mydemo.Controllers
{
    public class AuthUsersController : Controller
    {
        public readonly Day6MvcdbContext _context;
        [TempData]
        public string Error { get; set; }
        [TempData]
        public string Username { get; set; }
        [TempData]
        public string Password { get; set; }

        public AuthUsersController(Day6MvcdbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            Username = username;
            Password = password;
            if (username == null || password == null)
            {
                Error = "Must Enter Username & Password";
                return RedirectToAction("Index");
            }
            if (_context.Users.Any(u => u.Username == username && u.Password == password))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Error = "Invalid Username and/or Password";
                return RedirectToAction("Index");
            }
        }
    }
}
