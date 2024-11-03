using Day6Mydemo.Models;

namespace Day6Mydemo.Repositories.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        public Guid lifetime { get; set; }
    }
}
