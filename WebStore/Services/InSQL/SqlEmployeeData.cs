using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL;

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
        // _db.Employees.Add(employee); // влоб
        // _db.Add(employee) // добавляем прямо в контекст

        _db.Entry(employee).State = EntityState.Added; // помечаем объект как добавленый

        _db.SaveChanges(); // добавляется при любом варианте. только тут employee.Id получит значение

        return employee.Id;
    }

    public bool Delete(int id)
    {
        // var employee = GetById(id);
        // чтоб не тянуть объект целиком
        var employee = _db.Employees
            .Where(x => x.Id == id)
            .Select(x => new Employee
            {
                Id = x.Id,
            }).FirstOrDefault();

        if (employee is null)
        {
            _logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id:{0}", id);
            return false;
        }
        //_db.Entry(employee).State = EntityState.Deleted;
        //_db.Remove(employee) // через контекст бд
        _db.Employees.Remove(employee);

        _db.SaveChanges();
        _logger.LogWarning("Сотрудник с Id:{0} был удалён", id);
        return true;
    }

    public bool Edit(Employee employee)
    {
        _db.Employees.Update(employee);

        //****************************************

        //_db.Update(employee);

        //****************************************

        //_db.Entry(employee).State = EntityState.Modified;

        return _db.SaveChanges() != 0;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _db.Employees.AsEnumerable();
    }

    public Employee? GetById(int id) => _db.Employees.Find(id);//_db.Employees.Where(x => x.Id == id).FirstOrDefault();
                                                               // объект _db кэширует данные, Find обращается к ним, экономя ресурсы
}
