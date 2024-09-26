using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Day6Mydemo.Models
{
    public class EmployeeBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var employeename = bindingContext.HttpContext.Request.Form["EmployeeName"];
            var job = bindingContext.HttpContext.Request.Form["Job"];
            var salary = bindingContext.HttpContext.Request.Form["Salary"];
            var address = bindingContext.HttpContext.Request.Form["Address"];
            var email = bindingContext.HttpContext.Request.Form["Email"];
            var departId = bindingContext.HttpContext.Request.Form["DepartId"];

            if(!decimal.TryParse(salary, out _))
            {
                bindingContext.ModelState.AddModelError("Salary", "Invalid Salary");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }
            decimal newsalary = decimal.Parse(salary) * 1.1m;

            Employee employee = new Employee
            {
                EmployeeName = employeename,
                Job = job,
                Salary = newsalary,
                Address = address,
                Email = email,
                DepartId = int.Parse(departId)
            };
            bindingContext.Result = ModelBindingResult.Success(employee);
        }
    }
}
