using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers;

public class CatalogController : Controller
{
    private readonly IProductData _ProductData;

    public CatalogController(IProductData ProductData)
    {
        _ProductData = ProductData;
    }

    public IActionResult Index(int? BrandId, int? SectionId)
    {
        var filter = new ProductFilter 
        { 
            BrandId = BrandId,
            SectionId = SectionId,
        };

        var products = _ProductData.GetProducts(filter);

        var catalog_model = new CatalogViewModel 
        {
            BrandId = BrandId,
            SectionId = SectionId,
            Products = products
                .OrderBy(x => x.Order)
                .Select(x => new ProductViewModel 
                { 
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                }),
        };

        return View(catalog_model);
    }
}
