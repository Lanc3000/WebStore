using Microsoft.Extensions.DependencyInjection;
using TestConsole.Data;
using TestConsole.Services;
using TestConsole.Services.Interfaces;

var service_collections = new ServiceCollection();

service_collections.AddScoped<IDataManager, DataManager>();
service_collections.AddScoped<IDataProcessor, ConsolePrintProcessor>();

//service_collections.AddSingleton<IDataManager, DataManager>();
//service_collections.AddSingleton<IDataProcessor, WriteToFileProcessor>();
//service_collections.AddSingleton<IDataProcessor, ConsolePrintProcessor>();

//service_collection.AddTransient<>()
//service_collection.AddScoped<>()

var provider = service_collections.BuildServiceProvider();

var service = provider.GetRequiredService<IDataManager>();

using (var scope = provider.CreateScope())
{
    var scope_provider = scope.ServiceProvider;
    var service2 = scope_provider.GetRequiredService<IDataManager>();
    var is_equals = ReferenceEquals(service, service2);
}
    //var service2 = provider.GetRequiredService<IDataManager>();

    //var is_Equals = ReferenceEquals(service, service2);

    var data = Enumerable.Range(1, 100).Select(i => new DataValue
    {
        Id = i,
        Value = $"Value - {i}",
        Time = DateTime.Now.AddHours(-i * 10),
    });

service.ProcessData(data);

Console.ReadLine();