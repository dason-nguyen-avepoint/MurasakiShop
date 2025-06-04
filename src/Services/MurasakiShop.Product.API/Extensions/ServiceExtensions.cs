using Microsoft.EntityFrameworkCore;
using MurasakiShop.Product.API.DTOs;
using MurasakiShop.Product.API.Mappings;
using MurasakiShop.Product.API.Repositories;
using MurasakiShop.Product.API.Services;

namespace MurasakiShop.Product.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddLogging(config =>
        {
            config.AddConsole();
            config.AddDebug();
        });
    }
    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IProductService, ProductService>();
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IProductRepository, ProductRepository>();

    public static void ConfigureAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(typeof(AutoMapperProfile));

    public static void ConfigureSqlContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ProductContext>(options =>
            options.UseSqlServer(connectionString));
    }
}