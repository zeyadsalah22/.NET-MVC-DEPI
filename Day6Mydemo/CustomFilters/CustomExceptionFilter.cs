using Microsoft.AspNetCore.Mvc.Filters;

namespace Day6Mydemo.CustomFilters
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new  NotImplementedException();
        }
    }
}
