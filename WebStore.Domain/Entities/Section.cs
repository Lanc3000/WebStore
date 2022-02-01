using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Section : NamedEntity, IOrderedEntity
{
    public int Order { get; set; }
    public int? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))] // можно не писать, EF сама разберется
    public Section Parent { get; set; }

    public ICollection<Product> Products { get; set; }
}
