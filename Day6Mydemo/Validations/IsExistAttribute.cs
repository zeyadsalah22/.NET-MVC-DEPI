using System.ComponentModel.DataAnnotations;
using Day6Mydemo.Models;
namespace Day6Mydemo.Validations
{
    public class IsExistAttribute : ValidationAttribute
    {
        public string MyErrorMessage { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (Day6MvcdbContext)validationContext.GetService(typeof(Day6MvcdbContext));
            string name = value.ToString();
            Employee employee = _context.Employees.FirstOrDefault(e => e.EmployeeName==name);
            if (employee != null)
            {
                return new ValidationResult(MyErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
