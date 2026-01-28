using DapperAPI.Domain.Interfaces;
using DapperAPI.Infrastructure.Context;
using DapperAPI.Infrastructure.Interfaces;
using DapperAPI.Infrastructure.Repositories;
using DapperAPI.Service.Interfaces;
using DapperAPI.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DapperAPI.Service.Extensions;

public static class ResolveDependenciesExtensions
{

    public static IServiceCollection AddCustomDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(configuration.GetConnectionString("DefaultConnection") ?? "Data Source=dapperapi_temp.db"));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IPedidoService, PedidoService>();

        return services;
    }
}
