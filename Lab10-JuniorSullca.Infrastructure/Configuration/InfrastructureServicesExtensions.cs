using Lab10_JuniorSullca.Application.Interfaces;
using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Infrastructure.Persistence;
using Lab10_JuniorSullca.Infrastructure.Repositories;
using Lab10_JuniorSullca.Infrastructure.Services;
using Lab10_JuniorSullca.Infrastructure.Services.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10_JuniorSullca.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Db
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });
        
        // Repositories
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IResponseRepository, ResponseRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IExcelReportService, ExcelReportService>();
        services.AddScoped<IPruebaService, PruebaService>();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(IUnitOfWork).Assembly
        ));

        return services;
    }
}
