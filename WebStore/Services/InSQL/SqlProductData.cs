using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL;

public class SqlProductData : IProductData
{
    private readonly WebStoreDB _db;

    public SqlProductData(WebStoreDB db)
    {
        _db = db;
    }

    public Product CreateProduct(
        string Name, 
        int Order, 
        decimal Price, 
        string ImageUrl, 
        string Section, 
        string? Brand = null)
    {
        //var section = _db.Sections.FirstOrDefault(s => s.Name == Section);
        //if (section is null) 
        //{
        //    section = new Section { Name = Name };
        //}
        var section = _db.Sections.FirstOrDefault(s => s.Name == Section)
            ?? new Section { Name = Section };
        var brand = Brand is { Length: > 0 }
            ? _db.Brands.FirstOrDefault(s => s.Name == Brand)
            ?? new Brand { Name = Brand }
            : null;

        var product = new Product
        {
            Name = Name,
            Price = Price,
            Order = Order,
            ImageUrl = ImageUrl,
            Section = section,
            Brand = brand,
        };

        _db.Products.Add(product);
        _db.SaveChanges();

        return product;
    }

    public Brand? GetBrandById(int Id) => _db.Brands
        .Include(b => b.Products)
        .FirstOrDefault(b => b.Id == Id);

    public IEnumerable<Brand> GetBrands() => _db.Brands;

    public Product? GetProductById(int id) => _db.Products
        .Include(x => x.Brand)
        .Include(x => x.Section)
        .FirstOrDefault(x => x.Id == id);


    //_db.Products
    //.Where(x => x.Id == id)
    //.Select(x => new Product
    //{
    //    Id = x.Id,
    //    Section = x.Section,
    //    Brand = x.Brand
    //})
    //.FirstOrDefault();
    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        IQueryable<Product> query = _db.Products
            .Include(x => x.Brand)
            .Include(x => x.Section);

        if (Filter?.Ids.Length > 0)
        {
            query = query.Where(p => Filter.Ids!.Contains(p.Id));
        }
        else
        {
            if (Filter?.SectionId is { } section_id)
                query = query.Where(p => p.SectionId == section_id);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(p => p.BrandId == brand_id);
        }

        return query;
    }

    public Section? GetSectionById(int Id) => _db.Sections
        .Include(s => s.Products)
        .FirstOrDefault(s => s.Id == Id);

    public IEnumerable<Section> GetSections() => _db.Sections;
}
