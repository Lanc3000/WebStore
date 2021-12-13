using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new() 
        {
            new Employee { Id = 1, FirstName = "Иванов", LastName = "Иван", Patronymic = "Иванович", Age = 27},
            new Employee { Id = 2, FirstName = "Петров", LastName = "Петр", Patronymic = "Александрович", Age = 24},
            new Employee { Id = 3, FirstName = "Сидоров", LastName = "Владимир", Patronymic = "Юрьевич", Age = 36}
        };

        public IActionResult Index()
        {
            //return Content("Hello from controller!");
            return View();
        }

        public string ConfiguredAction(string id)
        {
            return $"akdgja - {id}";
        }

        public IActionResult Employees()
        {
            return View(_employees);
        }
        
        public IActionResult Employee(int id)
        {
            return View(_employees[id - 1]);
        }
    }
}
