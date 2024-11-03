using Day6Mydemo.Models;
using Day6Mydemo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day6Mydemo.Repositories.Implements
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public Guid lifetime { get; set; }
        private readonly Day6MvcdbContext _context;
        public DepartmentRepository(Day6MvcdbContext context)
        {
            lifetime = Guid.NewGuid();
            _context = context;
        }
        public void Create(Department entity)
        {
            _context.Departments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var entity = GetById(id);
            _context.Departments.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int? id)
        {
            return _context.Departments.FirstOrDefault(m => m.DepartmentId == id);
        }

        public void Update(Department entity)
        {
            _context.Departments.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
