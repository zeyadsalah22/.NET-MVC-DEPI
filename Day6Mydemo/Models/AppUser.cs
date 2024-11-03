using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Day6Mydemo.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        public string? City { get; set; }
    }
}
