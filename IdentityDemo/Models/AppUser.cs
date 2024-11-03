using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        public string City { get; set; }
    }
}
