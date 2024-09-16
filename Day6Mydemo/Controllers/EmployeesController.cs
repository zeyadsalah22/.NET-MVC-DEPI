using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day6Mydemo.Models;
using Day6Mydemo.ViewModels;
namespace Day6Mydemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly Day6MvcdbContext _context;
        [TempData]
        public string MessageAdd { get; set; }
        [TempData]
        public string MessageDelete { get; set; }
        public EmployeesController(Day6MvcdbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public IActionResult Index()
        {
            var day6MvcdbContext = _context.Employees.Include(e => e.Depart);
            return View(day6MvcdbContext.ToList());
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee =  _context.Employees
                .Include(e => e.Depart)
                .FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeId,EmployeeName,Job,Salary,Address,Email,DepartId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                _context.SaveChanges();
                MessageAdd = $"Employee {employee.EmployeeName} added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EmployeeId,EmployeeName,Job,Salary,Address,Email,DepartId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = _context.Employees
                .Include(e => e.Depart)
                .FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            _context.SaveChanges();
            MessageDelete = $"Employee {employee.EmployeeName} deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ShowEmployee(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = _context.Employees
                .Include(e => e.Depart)
                .FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeViewModel employeeViewModel = new EmployeeViewModel() {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Job = employee.Job,
                DepartmentName = employee.Depart.DepartmentName,
                DepartmentManager = employee.Depart.DepartmnetManager
            };
            return View(employeeViewModel);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
