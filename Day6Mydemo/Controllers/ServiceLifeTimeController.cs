using Day6Mydemo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Day6Mydemo.Controllers
{
    public class ServiceLifeTimeController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public ServiceLifeTimeController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        

        public IActionResult Index()
        {
            ViewBag.Lifetime = _departmentRepository.lifetime;
            return View();
        }
    }
}
