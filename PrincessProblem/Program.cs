using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrincessProblem;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<Princess>();
                services.AddScoped<Hall>();
                services.AddScoped<Friend>();
                services.AddScoped<ContendersGenerator>();
            });
    }
}