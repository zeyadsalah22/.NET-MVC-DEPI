using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day6Mydemo.Models;
using Day6Mydemo.ViewModels;
using Day6Mydemo.Repositories.Interfaces;
namespace Day6Mydemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        [TempData]
        public string MessageAdd { get; set; }
        [TempData]
        public string MessageDelete { get; set; }
        public EmployeesController(IEmployeeRepository repository, IDepartmentRepository departmentRepository)
        {
            // _context = context;
            _repository = repository;
            _departmentRepository = departmentRepository;
        }

        // GET: Employees
        public IActionResult Index()
        {
            return View(_repository.GetAllWithDepartments());
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee =  _repository.GetByIdWithDepartments(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["Depart_ID"] = new SelectList(_departmentRepository.GetAll(), "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([ModelBinder(typeof(EmployeeBinder))] Employee employee)
        {
            if (employee.Depart_ID==0)
            {
                ModelState.AddModelError("Depart_ID", "Please select a department");
            }
            if (ModelState.IsValid)
            {
                _repository.Create(employee);
                MessageAdd = $"Employee {employee.EmployeeName} added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["Depart_ID"] = new SelectList(_departmentRepository.GetAll(), "DepartmentId", "DepartmentName", employee.Depart_ID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Depart_ID"] = new SelectList(_departmentRepository.GetAll(), "DepartmentId", "DepartmentName", employee.Depart_ID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EmployeeId,EmployeeName,Job,Salary,Address,Email,Depart_ID")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(employee);
                    //_context.SaveChanges();
                    _repository.Update(employee);
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
            // ViewData["Depart_ID"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.Depart_ID);
            ViewData["Depart_ID"] = new SelectList(_departmentRepository.GetAll(), "DepartmentId", "DepartmentName", employee.Depart_ID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            //var employee = _context.Employees
            //    .Include(e => e.Depart)
            //    .FirstOrDefault(m => m.EmployeeId == id);
            var employee = _repository.GetById(id); 
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
            var employee = _repository.GetById(id);
            if (employee != null)
            {
                // _context.Employees.Remove(employee);
                _repository.Delete(id);
            }

            // _context.SaveChanges();
            MessageDelete = $"Employee {employee.EmployeeName} deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ShowEmployee(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //var employee = _context.Employees
            //    .Include(e => e.Depart)
            //    .FirstOrDefault(m => m.EmployeeId == id);
            var employee = _repository.GetByIdWithDepartments(id);
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
            return _repository.GetAll().Any(e => e.EmployeeId == id);
        }

        public IActionResult ShowEmployeeDetails(int? id)
        {
            var employee = _repository.GetByIdWithDepartments(id);
            
            return PartialView("EmployeeDetailsPartial",employee);
        } 
        public IActionResult ShowEmployees(int departmentId)
        {
            //  List<Employee> employeeList = _context.Employees.Where(e => e.Depart_ID == departmentId).ToList();
            List<Employee> employeeList = _repository.GetEmployeesByDepartment(departmentId).ToList();
            return Json(employeeList);
        }
    }
}
