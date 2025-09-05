using Ahatornn.TestGenerator;
using EquipHandover.Context.Tests;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace EquipHandover.Repositories.Tests;

/// <summary>
/// Тесты на <see cref="EquipmentReadRepository"/>
/// </summary>
public class EquipmentReadRepositoryTests : EquipHandoverContextInMemory
{
    private readonly IEquipmentReadRepository equipmentReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentReadRepositoryTests"/>
    /// </summary>
    public EquipmentReadRepositoryTests()
    {
        equipmentReadRepository = new EquipmentReadRepository(Context);
    }

    /// <summary>
    /// GetByIdAsync должен быть null
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldBeNull()
    {
        // Arrange
        var equipment1 = TestEntityProvider.Shared.Create<Equipment>();
        var equipment2 = TestEntityProvider.Shared.Create<Equipment>();
        var equipmentId = Guid.NewGuid();
        Context.AddRange(equipment1, equipment2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await equipmentReadRepository.GetByIdAsync(equipmentId, CancellationToken.None);

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
        var equipment1 = TestEntityProvider.Shared.Create<Equipment>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(equipment1);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await equipmentReadRepository.GetByIdAsync(equipment1.Id, CancellationToken.None);

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
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        Context.AddRange(equipment);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await equipmentReadRepository.GetByIdAsync(equipment.Id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(equipment);
    }
    
    /// <summary>
    /// GetByIdsAsync должен вернуть null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdsAsyncShouldReturnNullBySoftDelete()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(equipment);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await equipmentReadRepository.GetByIdsAsync(
            new List<Guid> { equipment.Id }, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
    
    /// <summary>
    /// GetByIdsAsync должен быть пустым
    /// </summary>
    [Fact]
    public async Task GetByIdsAsyncShouldBeEmpty()
    {
        // Arrange
        var equipment1 = TestEntityProvider.Shared.Create<Equipment>();
        var equipment2 = TestEntityProvider.Shared.Create<Equipment>();
        var equipmentIds = Guid.NewGuid();
        Context.AddRange(equipment1, equipment2);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await equipmentReadRepository.GetByIdsAsync(
            new List<Guid> { equipmentIds }, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
    
    /// <summary>
    /// GetByIdsAsync должен вернуть значения
    /// </summary>
    [Fact]
    public async Task GetByIdsAsyncShouldReturnValues()
    {
        // Arrange
        var equipment1 = TestEntityProvider.Shared.Create<Equipment>();
        var equipment2 = TestEntityProvider.Shared.Create<Equipment>();
        var equipment3 = TestEntityProvider.Shared.Create<Equipment>(x =>
            x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(equipment1, equipment2, equipment3);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await equipmentReadRepository.GetByIdsAsync(
            new List<Guid> { equipment1.Id, equipment2.Id, equipment3.Id }, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть пустой список
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldBeEmpty()
    {
        // Act
        var result =
            await equipmentReadRepository.GetAllAsync(CancellationToken.None);

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
        var equipment1 = TestEntityProvider.Shared.Create<Equipment>();
        var equipment2 = TestEntityProvider.Shared.Create<Equipment>();
        var equipment3 = TestEntityProvider.Shared.Create<Equipment>(x =>
            x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(equipment1, equipment2, equipment3);
        await UnitOfWork.SaveChangesAsync();
    
        // Act
        var result = await equipmentReadRepository.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }
}