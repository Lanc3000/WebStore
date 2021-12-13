namespace WebStore.Models;

public class Company
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Employee> Employees { get; set; } = new List<Employee>();
}

