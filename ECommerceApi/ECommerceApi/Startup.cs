using ECommerceApplication.Handlers;
using ECommerceApplication.Repositories;
using ECommerceApplication.Validators;
using ECommerceInfrastructure;
using ECommerceInfrastructure.Implementations;
using ECommercePresentation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("LocalConnection")));
        services.AddScoped<IProductRepository, PostgresProductRepository>();
        RegisterMediatorHandlers(services);
        services.AddControllers().AddApplicationPart(typeof(ECommerceController).Assembly);
        services.AddControllers();
        services.AddSwaggerGen();
    }

    private static void RegisterMediatorHandlers(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CreateProductHandler).Assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}