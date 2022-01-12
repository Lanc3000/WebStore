namespace WebStore.Infrastructure.Middleware;

public class TestMiddleware
{
    private readonly RequestDelegate _Next;
    public TestMiddleware(RequestDelegate Next)
    {
        _Next = Next;
    }

    public async Task Invoke(HttpContext Context)
    {
        var controllerName = Context.Request.RouteValues["controller"];
        var actionName = Context.Request.RouteValues["action"];
        //Обработка информации из Context.Request
        var processing_task = _Next(Context); // передаём управление следующему middleware в конвеере
        //выполнение каких-то действий параллельно с остальной частью конвеера
        await processing_task;
        //дообработка данных в Context.Response п
    }
}

