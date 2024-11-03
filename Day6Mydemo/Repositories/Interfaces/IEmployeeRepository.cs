using Day6Mydemo.Models;
using Microsoft.EntityFrameworkCore;

namespace Day6Mydemo.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public IEnumerable<Employee> GetAllWithDepartments();
        public Employee GetByIdWithDepartments(int? id);
        public IEnumerable<Employee> GetEmployeesByDepartment(int id);
    }
}
