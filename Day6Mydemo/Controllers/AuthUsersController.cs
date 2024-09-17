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
        public string Email { get; set; }
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

        public IActionResult Register(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
            if (username == null || email == null || password == null)
            {
                Error = "Must Enter Username & Email & Password";
                return RedirectToAction("Index");
            }
            if (_context.Users.Any(u => u.Username == username))
            {
                Error = "Username Already Exists";
                return RedirectToAction("Index");
            }
            if (_context.Users.Any(u => u.Email == email))
            {
                Error = "Email Already Exists";
                return RedirectToAction("Index");
            }
            User user = new User { Username = username, Email = email, Password = password };
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                Error = "Invalid Username, Email and/or Password";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
           return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Username = username;
            Password = password;
            if (username == null || password == null)
            {
                Error = "Must Enter Username & Password";
                return RedirectToAction("Login");
            }
            if (_context.Users.Any(u => u.Username == username && u.Password == password))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Error = "Invalid Username and/or Password";
                return RedirectToAction("Login");
            }
        }
    }
}
