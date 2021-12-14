namespace WebStore.Models;

public interface IEmployeeRepository
{
    IQueryable<Employee> Employees { get; }
}

