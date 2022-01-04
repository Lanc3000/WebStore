using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components;

// [ViewComponents(Name = "Секций")]
public class SectionsViewComponent : ViewComponent
{
    public IProductData _ProductData;
    public SectionsViewComponent(IProductData productData)
    {
        _ProductData = productData;
    }
    public IViewComponentResult Invoke() 
    {
        var sections = _ProductData.GetSections();

        var parent_sections = sections.Where(x => x.ParentId is null);

        var parent_sections_views = parent_sections
            .Select(x => new SectionViewModel 
                { 
                    Id = x.Id,
                    Name = x.Name,
                    Order = x.Order,
                }).ToList();

        foreach (var parent_section in parent_sections_views)
        {
            var child = sections.Where(x => x.ParentId == parent_section.Id);

            foreach (var children in child)
            {
                parent_section.ChildSections.Add(new SectionViewModel 
                { 
                    Id = children.Id,
                    Name = children.Name,
                    Order = children.Order,
                    Parent = parent_section,
                });
            }
            parent_section.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
        }
        parent_sections_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

        return View(parent_sections_views);
    } 
}
