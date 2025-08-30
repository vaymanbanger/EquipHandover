using Ahatornn.TestGenerator;
using EquipHandover.Context.Tests;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace EquipHandover.Repositories.Tests;

/// <summary>
/// Тесты на <see cref="SenderReadRepository"/>
/// </summary>
public class SenderReadRepositoryTests : EquipHandoverContextInMemory
{
    private readonly ISenderReadRepository senderReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderReadRepositoryTests"/>
    /// </summary>
    public SenderReadRepositoryTests()
    {
        senderReadRepository = new SenderReadRepository(Context);
    }
    
    /// <summary>
    /// GetByIdAsync должен быть null
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldBeNull()
    {
        // Arrange
        var sender1 = TestEntityProvider.Shared.Create<Sender>();
        var sender2 = TestEntityProvider.Shared.Create<Sender>();
        var senderId = Guid.NewGuid();
        await Context.AddRangeAsync(sender1, sender2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await senderReadRepository.GetByIdAsync(senderId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
    
    /// <summary>
    /// GetByIdAsync долежен быть null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldBeNullBySoftDelete()
    {
        // Arrange
        var sender1 = TestEntityProvider.Shared.Create<Sender>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddRangeAsync(sender1);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await senderReadRepository.GetByIdAsync(sender1.Id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    /// <summary>
    /// GetByIdAsync должен вернуть значение
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldReturnValue()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        await Context.AddRangeAsync(sender);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await senderReadRepository.GetByIdAsync(sender.Id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(sender);
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть пустой список
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldBeEmpty()
    {
        // Act
        var result =
            await senderReadRepository.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть список с данными
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldReturnValues()
    {
        // Arrange
        var sender1 = TestEntityProvider.Shared.Create<Sender>();
        var sender2 = TestEntityProvider.Shared.Create<Sender>();
        var sender3 = TestEntityProvider.Shared.Create<Sender>(x =>
            x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddRangeAsync(sender1, sender2, sender3);
        await UnitOfWork.SaveChangesAsync();
    
        // Act
        var result = await senderReadRepository.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }
}