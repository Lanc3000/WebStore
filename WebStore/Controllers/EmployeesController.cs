using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee { Id = 1, FirstName = "Иванов", LastName = "Иван", Patronymic = "Иванович", Age = 27, Salary = 120000 },
            new Employee { Id = 2, FirstName = "Петров", LastName = "Петр", Patronymic = "Александрович", Age = 24, Salary = 100000 },
            new Employee { Id = 3, FirstName = "Сидоров", LastName = "Владимир", Patronymic = "Юрьевич", Age = 36, Salary = 350000 }
        };
        public IActionResult Index()
        {          
            return View(__Employees);
        }

        //[Route("~/employees/info-{id}")]
        public IActionResult Details(int Id)
        {
            var employee = __Employees.FirstOrDefault(x => x.Id == Id);

            if (employee is null)
                return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }
    }
}
