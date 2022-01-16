using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory;

[Obsolete("Используйте класс WEbStore.Services.InSQL.SqlProductData", error: true)]
public class InMemoryProductData : IProductData
{
    public IEnumerable<Brand> GetBrands() => TestData.Brands;

    public IEnumerable<Section> GetSections() => TestData.Sections;

    public IEnumerable<Product> GetProducts(ProductFilter? Filters = null)
    {
        IEnumerable<Product> query = TestData.Products;

        //if (Filters?.SectionId != null)
        //    query = query.Where(x => x.SectionId == Filters.SectionId);

        if (Filters?.SectionId is { } section_id) // сахар из C#9
            query = query.Where(x => x.SectionId == section_id);

        if (Filters?.BrandId is { } brand_id)
            query = query.Where(x => x.BrandId == brand_id);

        return query;
    }
}
