using EquipHandover.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EquipHandover.Web.Tests.Infrastructure;

/// <summary>
/// Фибрика для веб приложения с конфигурацией
/// </summary>
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.CreateHost"/>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(TestsConstants.IntegrationEnvironment);
        return base.CreateHost(builder);
    }

    /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.ConfigureWebHost"/>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestAppConfiguration();
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<EquipHandoverContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddSingleton<DbContextOptions<EquipHandoverContext>>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("IntegrationConnection");
                var dbContextOptions =
                    new DbContextOptions<EquipHandoverContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionBuilder = new DbContextOptionsBuilder<EquipHandoverContext>(dbContextOptions)
                    .UseApplicationServiceProvider(provider)
                    .UseNpgsql(connectionString: string.Format(connectionString!,
                        Guid.NewGuid().ToString("N")));
                return optionBuilder.Options;
            });
        });
    } 
}