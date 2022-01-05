using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices]IProductData ProductData)
        {
            var products = ProductData.GetProducts()
                .OrderBy(x => x.Order)
                .Take(6)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                });
            ViewBag.Products = products;
            //return Content("Hello from controller!");
            //ControllerContext.HttpContext.Request.RouteValues
            return View();
        }

        public string ConfiguredAction(string id, string Value)
        {
            return $"Hello {id} - {Value}";
        }

        public void Throw(string Message) => throw new ApplicationException(Message);
    }
}
