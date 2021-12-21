using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

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
    }
}
