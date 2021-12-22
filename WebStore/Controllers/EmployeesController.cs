using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private ICollection<Employee> __Employees;
        public EmployeesController(ICollection<Employee> employees)
        {
            __Employees = employees;
        }
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

        //public IActionResult Create() => View();

        public IActionResult Edit(int id) 
        {
            var employee = __Employees.FirstOrDefault(x => x.Id == id);
            if (employee is null)
                return NotFound();

            var model = new EmployeeUpdateViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronomic = employee.Patronymic,
                Age = employee.Age,
            };

            return View(model);
        }

        public IActionResult Edit(EmployeeUpdateViewModel Model) 
        {
            // Обработка модели
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) => View();
    }
}
