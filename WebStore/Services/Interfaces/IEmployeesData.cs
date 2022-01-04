using WebStore.Models;

namespace WebStore.Services.Abstract;
/// <summary>
/// Основные круд оперции для модели Employee
/// </summary>
public interface IEmployeesData
{
    IEnumerable<Employee> GetAll();

    Employee? GetById(int id);

    int Add(Employee employee);

    bool Edit(Employee employee);

    bool Delete(int id);
}

