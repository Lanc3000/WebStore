using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Middleware;
using WebStore.Services;
using WebStore.Services.Abstract;
using WebStore.Services.InSQL;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

//services.AddControllersWithViews(opt =>
//{
//    opt.Conventions.Add(new TestConvention());   
//});
//services.AddMvc();
//services.AddControllers(); WebApi
//services.AddTransient<IEmployeeRepository, EmployeeRepository>();
services.AddControllersWithViews();

services.AddDbContext<WebStoreDB>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
services.AddTransient<IDbInitializer, DbInitializer>();

//services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
//services.AddSingleton<IProductData, InMemoryProductData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IEmployeesData, SqlEmployeeData>();

var app = builder.Build(); // Сборка приложения

await using (var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await db_initializer.InitializeAsync(RemoveBefore: false);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware")); Добавление промежуточного ПО простой способ
//app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true}); 
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<TestMiddleware>(); // добаление промежуточного ПО более сложный способ

app.UseWelcomePage("/welcome");
//Загрузка информации из файла конфигурации
//var configuration = app.Configuration;
//var props = configuration["CustomParam"];
//app.MapGet("/", () => app.Configuration["CustomParam"]);

app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе.");
});

//app.MapDefaultControllerRoute(); - определение стандартного маршрута (внизу сейчас написано тоже самое)
app.MapControllerRoute(name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
