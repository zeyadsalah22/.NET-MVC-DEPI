using Day6Mydemo.Models;
using Day6Mydemo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day6Mydemo.Repositories.Implements
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Day6MvcdbContext _context;
        public EmployeeRepository(Day6MvcdbContext context)
        {
            _context = context;
        }
        public void Create(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            _context.Employees.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public IEnumerable<Employee> GetAllWithDepartments()
        {
            return _context.Employees.Include(d=>d.Depart).ToList();
        }

        public Employee GetById(int? id)
        {
            return _context.Employees.FirstOrDefault(m => m.EmployeeId == id);
        }

        public Employee GetByIdWithDepartments(int? id)
        {
            return _context.Employees.Include(d=>d.Depart).FirstOrDefault(m => m.EmployeeId == id);
        }

        public void Update(Employee entity)
        {
            _context.Employees.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetEmployeesByDepartment(int id)
        {
            return _context.Employees.Where(e=>e.Depart_ID== id);
        }

    }
}
