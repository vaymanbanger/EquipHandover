using EquipHandover.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EquipHandover.Context.Tests;

/// <summary>
/// Класс <see cref="EquipHandoverContext"/> для тестов с базой в памяти. Один контекст на тест
/// </summary>
public abstract class EquipHandoverContextInMemory : IAsyncDisposable
{
    /// <summary>
    /// Контекст <see cref="EquipHandoverContext"/>
    /// </summary>
    protected EquipHandoverContext Context { get; }

    /// <inheritdoc cref="IUnitOfWork"/>
    protected IUnitOfWork UnitOfWork => Context;

    /// <summary>
    /// Иницализирует новый экземпляр <see cref="EquipHandoverContextInMemory"/>
    /// </summary>
    protected EquipHandoverContextInMemory()
    {
        var optionsBuilder = new DbContextOptionsBuilder<EquipHandoverContext>()
            .UseInMemoryDatabase($"EquipHandoverTests{Guid.NewGuid()}")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        Context = new EquipHandoverContext(optionsBuilder.Options);
    }

    /// <inheritdoc cref="IAsyncDisposable"/>
    public async ValueTask DisposeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.DisposeAsync();
    }
}