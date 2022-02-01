using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

//[Table("NameOfTable")]
[Index(nameof(Name), IsUnique = true)]
public class Brand : NamedEntity, IOrderedEntity
{
    //[Column("NameOfColumn")]
    public int Order { get; set; }

    public ICollection<Product> Products { get; set; }
}
