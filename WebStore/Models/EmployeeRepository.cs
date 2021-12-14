namespace WebStore.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IQueryable<Employee> Employees => new List<Employee>
        {
            new Employee { Id = 1, FirstName = "Иванов", LastName = "Иван", Patronymic = "Иванович", Age = 27, Salary = 120000 },
            new Employee { Id = 2, FirstName = "Петров", LastName = "Петр", Patronymic = "Александрович", Age = 24, Salary = 100000 },
            new Employee { Id = 3, FirstName = "Сидоров", LastName = "Владимир", Patronymic = "Юрьевич", Age = 36, Salary = 350000 }
        }.AsQueryable<Employee>();
    }
}
