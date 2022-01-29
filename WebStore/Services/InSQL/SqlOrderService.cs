using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services.InSQL;

public class SqlOrderService : IOrderService
{
    private readonly WebStoreDB _db;
    private readonly UserManager<User> _UserManager;

    public SqlOrderService(WebStoreDB db, UserManager<User> UserManager)
    {
        _db = db;
        _UserManager = UserManager;
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(string UserName, CancellationToken Cancel = default)
    {
        var orders = await _db.Orders
           .Include(x => x.User)
           .Include(x => x.Items)
           .ThenInclude(item => item.Product)
           .Where(x => x.User.UserName == UserName)
           .ToArrayAsync(Cancel)
           .ConfigureAwait(false);

        return orders;
    }

    public async Task<Order?> GetOrderByIdAsync(int Id, CancellationToken Cancel = default)
    {
        var order = await _db.Orders
           .Include(x => x.User)
           .Include(x => x.Items)
           .ThenInclude(item => item.Product)
           .FirstOrDefaultAsync(x => x.Id == Id, Cancel)
           .ConfigureAwait(false);

        return order;
    }

    public async Task<Order> CreateOrderAsync(
        string UserName,
        CartViewModel Cart,
        OrderViewModel OrderModel,
        CancellationToken Cancel = default)
    {
        var user = await _UserManager.FindByNameAsync(UserName).ConfigureAwait(false);

        if (user is null)
            throw new InvalidOperationException($"Пользователь с именем {UserName} не найден в БД");

        await using var transaction = await _db.Database.BeginTransactionAsync(Cancel).ConfigureAwait(false);

        var order = new Order
        {
            User = user,
            Address = OrderModel.Address,
            Phone = OrderModel.Phone,
            Description = OrderModel.Description,
        };

        var products_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

        var cart_products = await _db.Products
           .Where(p => products_ids.Contains(p.Id))
           .ToArrayAsync(Cancel)
           .ConfigureAwait(false);

        order.Items = Cart.Items.Join( //присоединяем к Cart.Items записи из cart_products
            cart_products,
            cart_item => cart_item.Product.Id, // свойство-селектор из Cart.Items
            cart_product => cart_product.Id, // свойство-селектор из cart_products
            (cart_item, cart_product) => new OrderItem // новый объект, который получается в результате соединения
            {
                Order = order,
                Product = cart_product,
                Price = cart_product.Price, // Здесь может быть применена скидка к стоимости товара
                Quantity = cart_item.Quantity,
            }).ToArray();

        await _db.Orders.AddAsync(order, Cancel).ConfigureAwait(false);

        await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);

        await transaction.CommitAsync(Cancel).ConfigureAwait(false);

        return order;
    }
}
