using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    [Authorize] // даёт доступ только если авторизован
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(IEmployeesData EmployeesData, 
            ILogger<EmployeesController> logger)
        {
            _EmployeesData = EmployeesData;
            _logger = logger;
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

        //[Authorize(Roles = "Admin")]
        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        [HttpGet]
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Edit(int? id) 
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _EmployeesData.GetById((int)id);
            if (employee is null)
            {
                _logger.LogWarning("Попытка редактирования сотрудника с Id:{0} - сотрудник отсутствует", employee.Id);
                return NotFound();
            }
                

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Edit(EmployeeViewModel Model) 
        {
            if (Model.LastName == "Асама" && Model.Name == "Бен" && Model.Patronymic == "Ладен")
                ModelState.AddModelError("", "Террористов на работу не берём!");

            if (!ModelState.IsValid)
                return View(Model);

            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.Name,
                Patronymic = Model.Patronymic,
                Age = Model.Age,
            };

            if (Model.Id == 0)
            {
                _EmployeesData.Add(employee);
                _logger.LogInformation("Создан сотрудник {0}", employee);
            }
            else if (!_EmployeesData.Edit(employee))
            { 
                _logger.LogInformation("Информация о сотруднике {0} изменена", employee);
                return NotFound();


            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Administrators)]
        public IActionResult Delete(int id) 
        {
            if (id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };


            return View(model);
        }

        [Authorize(Roles = Role.Administrators)]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_EmployeesData.Delete(id))
                return NotFound();

            _logger.LogWarning("Удалён сотрудник с id{0}", id);

            return RedirectToAction("Index");
        }
    }
}
