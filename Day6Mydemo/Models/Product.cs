using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Day6Mydemo.Models
{
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Enter Your Product Name ")]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Your Price ")]
        [Remote("CheckPrice", "Products", ErrorMessage = "Price should be less than 100000")]
        public decimal Price { get; set; }
        public string? Photo { get; set; }
    }
}
