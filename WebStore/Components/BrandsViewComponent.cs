using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components;

public class BrandsViewComponent : ViewComponent
{
    private readonly IProductData _ProductData;

    public BrandsViewComponent(IProductData ProductData)
    {
        _ProductData = ProductData;
    }

    public IViewComponentResult Invoke() => View(GetBrands());

    private IEnumerable<BrandViewModel> GetBrands() =>
        _ProductData.GetBrands()
            .OrderBy(x => x.Order)
            .Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });
}
