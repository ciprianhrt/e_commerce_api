using Microsoft.AspNetCore;

namespace ECommerceApi;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var webHostBuilder = CreateWebHostBuilder(args).Build();
        await webHostBuilder.RunAsync();
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseWebRoot("")
            .UseStartup<Startup>();
} 