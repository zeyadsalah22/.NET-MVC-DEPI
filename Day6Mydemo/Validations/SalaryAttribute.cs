using System.ComponentModel.DataAnnotations;

namespace Day6Mydemo.Validations
{
    public class SalaryAttribute
    {
        public static ValidationResult ValidateSalary(decimal salary, ValidationContext context)
        {
            if (salary < 5000)
            {
                return new ValidationResult("Salary must be greater than 5000");
            }
            return ValidationResult.Success;
        }
    }
}
