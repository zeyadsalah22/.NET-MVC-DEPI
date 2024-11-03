using Microsoft.AspNetCore.Mvc.Filters;

namespace Day6Mydemo.CustomFilters
{
    public class CustomResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Resource executed");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Resource executing");
        }
    }
}
