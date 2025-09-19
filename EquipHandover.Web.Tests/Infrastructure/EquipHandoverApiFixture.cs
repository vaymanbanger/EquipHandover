using EquipHandover.Context;
using EquipHandover.Context.Contracts;
using EquipHandover.Web.Tests.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EquipHandover.Web.Tests.Infrastructure;

/// <summary>
/// Модификация фикстуры для интеграционных тестов
/// </summary>
public class EquipHandoverApiFixture : IAsyncLifetime
{
    private readonly TestWebApplicationFactory factory;
    private EquipHandoverContext? context;
    private IServiceScope? scope;
    private IUnitOfWork? unitOfWork;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverApiFixture"/>
    /// </summary>
    public EquipHandoverApiFixture()
    {
        factory = new TestWebApplicationFactory();
    }

    /// <summary>
    /// HTTP-клиент для выполнения запросов к тестовому API
    /// </summary>
    internal IEquipHandoverApiClient WebClient
    {
        get
        {
            var client = factory.CreateClient();
            return new EquipHandoverApiClient(string.Empty, client);
        }
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
            
            scope ??= factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            context = scope.ServiceProvider.GetRequiredService<EquipHandoverContext>();
            return context;
        }
    }
    
    /// <summary>
    /// Предоставляет доступ к UnitOfWork
    /// </summary>
    internal IUnitOfWork UnitOfWork
    {
        get
        {
            if (unitOfWork != null)
            {
                return unitOfWork;
            }
            
            scope ??= factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            return unitOfWork;
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