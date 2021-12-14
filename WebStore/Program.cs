using WebStore.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();
services.AddTransient<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true}); 
app.UseStaticFiles();

app.UseRouting();
//Загрузка информации из файла конфигурации
//var configuration = app.Configuration;
//var props = configuration["CustomParam"];
//app.MapGet("/", () => app.Configuration["CustomParam"]);
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе.");
});

//app.MapDefaultControllerRoute();
app.MapControllerRoute(name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
