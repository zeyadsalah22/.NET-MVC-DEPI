using Day6Mydemo.Models;
using Day6Mydemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Day6Mydemo.Controllers
{
    public class AuthDemoController : Controller
    {
        public AuthDemoController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public UserManager<AppUser> _userManager { get; }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    City = registerViewModel.City,
                    PasswordHash = registerViewModel.Password
                };
                IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if(result.Succeeded)
                {
                    //cookies
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerViewModel);
        }
    }
}
