using ConsoleAppWithSerilogAndDI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Console.WriteLine("Hello, World!");
var host = AppStartup();
var dataService = ActivatorUtilities.CreateInstance<DataRepository>(host.Services);
dataService.Connect();
    
static void ConfigSetup(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"appsettings.json"), optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
}

static IHost AppStartup()
{
    var builder = new ConfigurationBuilder();
    ConfigSetup(builder);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Build())
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddScoped<IDataRepository, DataRepository>();
                    })
                    .UseSerilog()
                    .Build();
    return host;
}