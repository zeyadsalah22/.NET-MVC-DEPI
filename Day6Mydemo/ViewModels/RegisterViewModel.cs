using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Day6Mydemo.ViewModels
{
    [Keyless]
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

    }
}
