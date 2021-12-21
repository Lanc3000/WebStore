using WebStore.Models;

namespace WebStore.Data;
public static class TestData
{
    public static List<Employee> __Employees { get; } = new()
    {
        new Employee { Id = 1, FirstName = "Иванов", LastName = "Иван", Patronymic = "Иванович", Age = 27, Salary = 120000 },
        new Employee { Id = 2, FirstName = "Петров", LastName = "Петр", Patronymic = "Александрович", Age = 24, Salary = 100000 },
        new Employee { Id = 3, FirstName = "Сидоров", LastName = "Владимир", Patronymic = "Юрьевич", Age = 36, Salary = 350000 }
    };
}

