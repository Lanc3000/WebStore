using WebStore.Data;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces;

public class InMemoryProductData : IProductData
{
    public IEnumerable<Brand> GetBrands() => TestData.Brands;

    public IEnumerable<Section> GetSections() => TestData.Sections;
}
