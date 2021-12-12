var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Загрузка информации из файла конфигурации
//var configuration = app.Configuration;
//var props = configuration["CustomParam"];

app.MapGet("/", () => app.Configuration["CustomParam"]);

app.Run();
