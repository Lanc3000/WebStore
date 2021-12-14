using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        //Company google = new Company { Id = 1, Title = "Google" };
        //Company apple = new Company { Id = 2, Title = "Apple" };

        //private IEmployeeRepository _employeeRepository;
        //public HomeController(IEmployeeRepository employeeRepository)
        //{
        //    _employeeRepository = employeeRepository;
        //}

        public IActionResult Index()
        {
            //return Content("Hello from controller!");
            return View();
        }

        public string ConfiguredAction(string id)
        {
            return $"akdgja - {id}";
        }

        //public IActionResult Employees()
        //{
        //    return View(_employeeRepository.Employees);
        //}

        //public IActionResult Employee(int id)
        //{
        //    return View(_employeeRepository.Employees.ElementAt(id - 1));
        //}
    }
}
