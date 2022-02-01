using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Services.Abstract;

namespace WebStore.Services.InSQL
{
    public class SqlEmployeeData : IEmployeesData
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<SqlEmployeeData> _logger;

        public SqlEmployeeData(WebStoreDB db, 
            ILogger<SqlEmployeeData> logger)
        {
            _db = db;
            _logger = logger;
        }

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            _db.Employees.Add(employee);

            return employee.Id;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
            {
                _logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id:{0}", id);
                return false;
            }

            _db.Employees.Remove(employee);

            _logger.LogWarning("Сотрудник с Id:{0} был удалён", id);
            return true;
        }

        public bool Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            var db_employee = GetById(employee.Id);
            if (db_employee is null)
            {
                _logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id:{0}", employee.Id);
                return false;
            }

            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;

            _db.Employees.Update(db_employee);
            _db.SaveChanges();

            _logger.LogInformation("Информация о сотруднике id: {0} была изменена", employee.Id);

            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees.ToList();
        }

        public Employee? GetById(int id) => _db.Employees.Where(x => x.Id == id).FirstOrDefault();
    }
}
