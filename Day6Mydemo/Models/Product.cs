using System.ComponentModel.DataAnnotations;

namespace Day6Mydemo.Models
{
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Enter Your Product Name ")]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Your Price ")]
        public decimal Price { get; set; }

        public string? Photo { get; set; }
    }
}
