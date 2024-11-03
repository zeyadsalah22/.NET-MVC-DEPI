using Day6Mydemo.Models;

namespace Day6Mydemo.ViewModels
{
    public class DepartmentPaginationViewModel
    {
        public List<Department> Departments{ get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalRecords, PageSize));
    }
}
