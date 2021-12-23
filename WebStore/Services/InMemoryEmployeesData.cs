using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Abstract;

namespace WebStore.Services;

public class InMemoryEmployeesData : IEmployeesData
{
    private ICollection<Employee> _Employees;
    private int _MaxFreeId;
    private readonly ILogger<InMemoryEmployeesData> _logger;

    public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> logger)
    {
        _Employees = TestData.Employees;
        _MaxFreeId = _Employees.DefaultIfEmpty().Max(x => x?.Id ?? 0) + 1;
        _logger = logger;
    }
    public int Add(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        if (_Employees.Contains(employee)) // избавиться при переходе к бд
            return employee.Id;

        employee.Id = _MaxFreeId++;
        _Employees.Add(employee);

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
            

        _Employees.Remove(employee);
        _logger.LogWarning("Сотрудник с Id:{0} был удалён", id);
        return true;
    }

    public bool Edit(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        if (_Employees.Contains(employee)) // избавиться при наличии бд
            return true;

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

        // когда будет бд вызывать SaveChanges

        _logger.LogInformation("Информация о сотруднике id: {0} была изменена", employee.Id);

        return true;
    }

    public IEnumerable<Employee> GetAll() => _Employees;

    public Employee? GetById(int id) => _Employees.FirstOrDefault(x => x.Id == id);
}

