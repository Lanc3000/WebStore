using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

public class Employee : IEntity, IOrderedEntity
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
    public int Order { get; set; }
}

