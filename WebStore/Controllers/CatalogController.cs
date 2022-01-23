using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebStore.Domain;
using WebStore.Mapping;
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
                .ToView()
        };

        return View(catalog_model);
    }

    public IActionResult Details(int id)
    {
        var product = _ProductData.GetProductById(id);
        
        //CultureInfo.CurrentUICulture = 
        //    CultureInfo.CurrentCulture = 
        //        CultureInfo.GetCultureInfo("ru-Ru");
        
        if (product is null)
            return NotFound();

        return View(product.ToView());
    }
}
