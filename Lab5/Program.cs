using Lab5.Application.Extensions;
using Lab5.Infrastructure.DataAccess.Extensions;
using Lab5.Presentation.Console;
using Lab5.Presentation.Console.Extenctions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Spectre.Console;

var collection = new ServiceCollection();
string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "..", "Applications.json");
string json = await File.ReadAllTextAsync(path);

var data = JObject.Parse(json);

collection
    .AddApplication()
    .AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = data["Host"]?.Value<string>() ?? throw new InvalidOperationException();
        configuration.Port = data["Port"]?.Value<int>() ?? throw new InvalidOperationException();
        configuration.Username = data["Username"]?.Value<string>() ?? throw new InvalidOperationException();
        configuration.Password = data["Password"]?.Value<string>() ?? throw new InvalidOperationException();
        configuration.Database = data["Database"]?.Value<string>() ?? throw new InvalidOperationException();
        configuration.SslMode = data["SslMode"]?.Value<string>() ?? throw new InvalidOperationException();
    })
    .AddPresentationConsole();

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

await scope.UseInfrastructureDataAccess();

ScenarioRunner scenarioRunner = scope.ServiceProvider
    .GetRequiredService<ScenarioRunner>();

while (true)
{
    await scenarioRunner.Run();
    AnsiConsole.Clear();
}