using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapidemo.Data;
using webapidemo.Models;

namespace webapidemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly AppDbContext _context;
		public EmployeeController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Employees.ToList());
		}

		[HttpGet("{id:int}")]
		public IActionResult GetById(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var emp = _context.Employees.FirstOrDefault(e => e.Id == id);
			if (emp == null)
			{
				return NotFound();
			}
			return Ok(emp);
		}

		[HttpGet("{name:alpha}")]
		public IActionResult GetByName(string name)
		{
			if (name == null)
			{
				return BadRequest();
			}
			var emp = _context.Employees.FirstOrDefault(e => e.Name.Contains(name));
			if (emp == null)
			{
				return NotFound();
			}
			return Ok(emp);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Employee employee)
		{
			if (ModelState.IsValid)
			{
				_context.Employees.Add(employee);
				_context.SaveChanges();
				return Created();
			}
			return BadRequest(ModelState);
		}

		[HttpPut("{id:int}")]
		public IActionResult Put([FromRoute] int id, [FromBody] Employee employee)
		{
			if (id == 0 || id != employee.Id)
			{
				return BadRequest();
			}
			if (!EmployeeExist(id))
			{
				return NotFound();
			}
			try
			{
				if (ModelState.IsValid)
				{
					_context.Entry(employee).State = EntityState.Modified;
					_context.SaveChanges();
					return Ok(employee);
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var emp = _context.Employees.FirstOrDefault(e => e.Id == id);
			if (emp == null)
			{
				return NotFound();
			}
			_context.Employees.Remove(emp);
			_context.SaveChanges();
			return NoContent();
		}

		private bool EmployeeExist(int id)
		{
			return _context.Employees.Any(e => e.Id == id);
		}

	}
}
