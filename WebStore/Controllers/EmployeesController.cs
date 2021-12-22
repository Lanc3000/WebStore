using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Services.Abstract;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }
        public IActionResult Index()
        {    
            return View(_EmployeesData.GetAll());
        }

        //[Route("~/employees/info-{id}")]
        public IActionResult Details(int Id)
        {
            var employee = _EmployeesData.GetById(Id);

            if (employee is null)
                return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

        //public IActionResult Create() => View();

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var employee = _EmployeesData.GetById(id);
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
        [HttpPost]
        public IActionResult Edit(EmployeeUpdateViewModel Model) 
        {
            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.Name,
                Patronymic = Model.Patronomic,
                Age = Model.Age,
            };

            if (!_EmployeesData.Edit(employee))
                return NotFound();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) 
        {
            return View();
        }
    }
}
