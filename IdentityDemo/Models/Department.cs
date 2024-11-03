using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IdentityDemo.Models
{
    public partial class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [DisplayName("Department")]
        [Required(ErrorMessage = "Please Enter the Department Name ")]
        [MaxLength(50, ErrorMessage = "Please Enter Name Less than  Letters ")]
        public string DepartmentName { get; set; } = null!;

        [DisplayName("Manager")]
        public string? DepartmnetManager { get; set; }

    }
}
