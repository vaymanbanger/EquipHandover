using Ahatornn.TestGenerator;
using EquipHandover.Context.Tests;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace EquipHandover.Repositories.Tests;

/// <summary>
/// Тесты на <see cref="ReceiverReadRepository"/>
/// </summary>
public class ReceiverReadRepositoryTests : EquipHandoverContextInMemory
{
    private readonly IReceiverReadRepository receiverReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverReadRepositoryTests"/>
    /// </summary>
    public ReceiverReadRepositoryTests()
    {
        receiverReadRepository = new ReceiverReadRepository(Context);
    }
    
    /// <summary>
    /// GetByIdAsync должен быть null
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldBeNull()
    {
        // Arrange
        var receiver1 = TestEntityProvider.Shared.Create<Receiver>();
        var receiver2 = TestEntityProvider.Shared.Create<Receiver>();
        var receiverId = Guid.NewGuid();
        Context.AddRange(receiver1, receiver2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await receiverReadRepository.GetByIdAsync(receiverId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
    
    /// <summary>
    /// GetByIdAsync должен быть null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldBeNullBySoftDelete()
    {
        // Arrange
        var receiver1 = TestEntityProvider.Shared.Create<Receiver>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(receiver1);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await receiverReadRepository.GetByIdAsync(receiver1.Id, CancellationToken.None);

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
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        Context.AddRange(receiver);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await receiverReadRepository.GetByIdAsync(receiver.Id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(receiver);
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть пустой список
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldBeEmpty()
    {
        // Act
        var result =
            await receiverReadRepository.GetAllAsync(CancellationToken.None);

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
        var receiver1 = TestEntityProvider.Shared.Create<Receiver>();
        var receiver2 = TestEntityProvider.Shared.Create<Receiver>();
        var receiver3 = TestEntityProvider.Shared.Create<Receiver>(x =>
            x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(receiver1, receiver2, receiver3);
        await UnitOfWork.SaveChangesAsync();
    
        // Act
        var result = await receiverReadRepository.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }
}