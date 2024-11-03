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
            var Depart_ID = int.Parse(bindingContext.HttpContext.Request.Form["Depart_ID"]);

            if (!decimal.TryParse(salary, out _))
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
                Depart_ID = Depart_ID
            };

            //if (Depart_ID == 0)
            //{
            //    bindingContext.ModelState.AddModelError("Depart_ID", "Please select a department"); // Add error message to ModelState
            //    bindingContext.Result = ModelBindingResult.Failed();
            //    return;
            //}
            
            bindingContext.Result = ModelBindingResult.Success(employee);
        }
    }
}
