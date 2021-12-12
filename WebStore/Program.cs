var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
//Загрузка информации из файла конфигурации
//var configuration = app.Configuration;
//var props = configuration["CustomParam"];
//app.MapGet("/", () => app.Configuration["CustomParam"]);
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе.");
});

app.MapDefaultControllerRoute();
app.Run();
