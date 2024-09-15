using System.ComponentModel.DataAnnotations;

namespace Day6Mydemo.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Job { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentManager { get; set; }
    }
}
