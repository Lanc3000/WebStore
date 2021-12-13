namespace WebStore.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IQueryable<Employee> Employees => new List<Employee>
        {
            new Employee { Id = 1, FirstName = "Иванов", LastName = "Иван", Patronymic = "Иванович", Age = 27 },
            new Employee { Id = 2, FirstName = "Петров", LastName = "Петр", Patronymic = "Александрович", Age = 24 },
            new Employee { Id = 3, FirstName = "Сидоров", LastName = "Владимир", Patronymic = "Юрьевич", Age = 36 }
        }.AsQueryable<Employee>();
    }
}
