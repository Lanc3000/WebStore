using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Middleware;
using WebStore.Services;
using WebStore.Services.Abstract;
using WebStore.Services.InCookies;
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


services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<WebStoreDB>()
    .AddDefaultTokenProviders();

services.Configure<IdentityOptions>(opt => 
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif
    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ123456789";
    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebStore.GB";
    opt.Cookie.HttpOnly = true;

    opt.ExpireTimeSpan = TimeSpan.FromDays(10);
    // opt.Cookie.Expiration = TimeSpan.FromDays(10); - устарел и больше не используется

    opt.LoginPath = "/Accaunt/Login";
    opt.LogoutPath = "/Accaunt/Logout";
    opt.AccessDeniedPath = "/Accaunt/AccessDenied";

    opt.SlidingExpiration = true;
});

//services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
//services.AddSingleton<IProductData, InMemoryProductData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IEmployeesData, SqlEmployeeData>();
services.AddScoped<ICartService, InCookiesCartService>();

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

app.UseAuthentication();
app.UseAuthorization();

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
