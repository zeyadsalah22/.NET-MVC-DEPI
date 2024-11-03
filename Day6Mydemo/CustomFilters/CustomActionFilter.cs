using Microsoft.AspNetCore.Mvc.Filters;

namespace Day6Mydemo.CustomFilters
{
    public class CustomActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action executed");
            var controller = context.RouteData.Values["Controller"].ToString();
            var action = context.RouteData.Values["Action"].ToString();
            Console.WriteLine($"Controller: {controller}, Action: {action}");
            context.HttpContext.Items["ControllerName"] = controller;
            context.HttpContext.Items["ActionName"] = action;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action executing");
        }
    }
}
