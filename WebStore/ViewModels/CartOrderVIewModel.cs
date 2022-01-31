namespace WebStore.ViewModels;

public class CartOrderViewModel // Композитная модель
{
    public CartViewModel Cart { get; set; } = null!;

    public OrderViewModel Order { get; set; } = new();
}
