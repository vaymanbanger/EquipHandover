using EquipHandover.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EquipHandover.Web.Tests.Infrastructure;

/// <summary>
/// Фикстура для интеграционных тестов
/// </summary>
public class EquipHandoverApiFixture : IAsyncLifetime
{
    private readonly TestWebApplicationFactory factory;
    private EquipHandoverContext? context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverApiFixture"/>
    /// </summary>
    public EquipHandoverApiFixture()
    {
        factory = new TestWebApplicationFactory();
    }

    /// <summary>
    /// Предоставляет доступ к контексту базы данных
    /// </summary>
    internal EquipHandoverContext Context
    {
        get
        {
            if (context != null)
            {
                return context;
            }
            
            var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            context = scope.ServiceProvider.GetRequiredService<EquipHandoverContext>();
            return context;
        }
    }

    /// <inheritdoc cref="IAsyncLifetime.InitializeAsync"/>
    public Task InitializeAsync() => Context.Database.MigrateAsync(); 

    /// <inheritdoc cref="IAsyncLifetime.DisposeAsync"/>
    public async Task DisposeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.Database.CloseConnectionAsync();
        await Context.DisposeAsync();
        await factory.DisposeAsync();
    }
}