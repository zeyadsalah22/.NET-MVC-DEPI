using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day6Mydemo.Models;
using Day6Mydemo.Repositories.Interfaces;
using Day6Mydemo.ViewModels;

namespace Day6Mydemo.Controllers
{
    public class DepartmentsController : Controller
    {
        [TempData]
        public string MessageAdd { get; set; }
        [TempData]
        public string MessageDelete { get; set; }

        // private readonly Day6MvcdbContext _context;
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        // GET: Departments
        public IActionResult Index()
        {
            //return View( _context.Departments.ToList());
            return View(_repository.GetAll());
        }

        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var department =  _repository.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("DepartmentId,DepartmentName,DepartmnetManager")] Department department)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(department);
                MessageAdd = $"Department {department.DepartmentName} added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var department =  _repository.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("DepartmentId,DepartmentName,DepartmnetManager")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(department);
                    //_context.SaveChanges();
                    _repository.Update(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var department =  _repository.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _repository.GetById(id);
            if (department != null)
            {
                // _context.Departments.Remove(department);
                _repository.Delete(id);
            }

            // _context.SaveChanges();
            MessageDelete = $"Department {department.DepartmentName} deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _repository.GetAll().Any(e => e.DepartmentId == id);
        }

        public IActionResult ShowDepartments()
        {
            List<Department> departmentList = _repository.GetAll().ToList();
            return View(departmentList);
        }

        public IActionResult GetDepartments(int pageNumber = 1, int pageSize=5) {
            var totalRecords = _repository.GetAll().Count();
            var departmentList = _repository.GetAll()
                .OrderBy(x => x.DepartmentId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
            var viewModel = new DepartmentPaginationViewModel
            {
                Departments = departmentList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
            return PartialView("_departmentListPartial", viewModel);
        }
        
    }
}
