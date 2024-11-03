using Microsoft.AspNetCore.Mvc.Filters;

namespace Day6Mydemo.CustomFilters
{
    public class CustomActionFilterAsync : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
        }
    }
}
