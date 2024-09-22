using System.ComponentModel.DataAnnotations;

namespace Day6Mydemo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Must enter Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Must enter Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
