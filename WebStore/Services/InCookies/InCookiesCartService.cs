using Newtonsoft.Json;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Mapping;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services.InCookies;

public class InCookiesCartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductData _productData;

    //определим название кукис:
    private readonly string _CartName;

    private Cart Cart
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;
            var cookies = context!.Response.Cookies; // берем куки которые готовятся к отправке

            var cartCookie = context.Request.Cookies[_CartName];

            if (cartCookie is null)
            {
                var cart = new Cart();
                cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                return cart;
            }
            ReplaceCart(cookies, cartCookie);
            return JsonConvert.DeserializeObject<Cart>(cartCookie)!;
        }
        set => ReplaceCart(_httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
    }

    private void ReplaceCart(IResponseCookies cookies, string cart)
    {
        cookies.Delete(_CartName);
        cookies.Append(_CartName, cart);
    }

    // внутри сервисов у нас нет доступа к контексту (например не можем сделать так HttpContext.Response.Append();)
    // можно получить доступ к кукис через сервис IHttpContextAccessor:
    public InCookiesCartService(IHttpContextAccessor httpContextAccessor, IProductData productData)
    {
        _httpContextAccessor = httpContextAccessor;
        _productData = productData;

        var user = httpContextAccessor.HttpContext!.User;
        var userName = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

        _CartName = $"WebStore.Cart{userName}";
    }
    public void Add(int id)
    {
        var cart = Cart;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
        if (item is null)
            cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
        else
            item.Quantity++;

        Cart = cart;
    }

    public void Decrement(int id)
    {
        var cart = Cart;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
        if (item is null)
            return;

        if (item.Quantity > 0)
            item.Quantity--;
        if (item.Quantity == 0)
            cart.Items.Remove(item);

        Cart = cart;
    }

    public void Remove(int id)
    {
        var cart = Cart;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
        if (item is null)
            return;

        cart.Items.Remove(item);

        Cart = cart;
    }

    public void Clear()
    {
        //Cart = new();

        var cart = Cart;

        cart.Items.Clear();
        
        Cart = cart;
    }

    public CartViewModel GetViewModel()
    {
        var cart = Cart;
        var products = _productData.GetProducts(new()
        {
            Ids = cart.Items.Select(c => c.ProductId).ToArray()
        });

        var productsViews = products.ToView().ToDictionary(p => p!.Id);

        return new() 
        { 
            Items = cart.Items
                .Where(item => productsViews.ContainsKey(item.ProductId))
                .Select(item => (productsViews[item.ProductId], item.Quantity))!
        };
    }
}
