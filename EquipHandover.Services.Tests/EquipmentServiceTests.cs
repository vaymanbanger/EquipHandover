using Ahatornn.TestGenerator;
using AutoMapper;
using EquipHandover.Common.Contracts;
using EquipHandover.Context.Tests;
using EquipHandover.Entities;
using EquipHandover.Repositories.ReadRepositories;
using EquipHandover.Repositories.WriteRepositories;
using EquipHandover.Services.AutoMappers;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Equipment;
using FluentAssertions;
using Moq;
using Xunit;

namespace EquipHandover.Services.Tests;

/// <summary>
/// Тесты на <see cref="EquipmentService"/>
/// </summary>
public class EquipmentServiceTests : EquipHandoverContextInMemory
{
    private readonly IEquipmentService equipmentService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentServiceTests"/>
    /// </summary>
    public EquipmentServiceTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });
        
        var mapper = config.CreateMapper();
        equipmentService = new EquipmentService(mapper,
            UnitOfWork,
            new EquipmentReadRepository(Context),
            new EquipmentWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id обудования с мягким удалением
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionBySoftDelete()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>(x =>
        {
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        await Context.AddRangeAsync(equipment);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => equipmentService.GetByIdAsync(equipment.Id, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id оборудования
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionByEquipmentId()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        await Context.AddRangeAsync(equipment);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => equipmentService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }

    /// <summary>
    /// GetByIdAsync должен вернуть значение
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldReturnValue()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        await Context.AddRangeAsync(equipment);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await equipmentService.GetByIdAsync(equipment.Id, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(equipment, options => options.ExcludingMissingMembers());
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть значения
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldReturnValues()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        var equipment1= TestEntityProvider.Shared.Create<Equipment>();
        var equipment2 = TestEntityProvider.Shared.Create<Equipment>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddRangeAsync(equipment, equipment1, equipment2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await equipmentService.GetAllAsync(CancellationToken.None);
        
        // Assert
        result.Should().HaveCount(2);
    }
    
    /// <summary>
    /// GetAllAsync должен быть пустым
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldBeEmpty()
    {
        // Act
        var result = await equipmentService.GetAllAsync(CancellationToken.None);
        
        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// CreateAsync должен создать оборудование
    /// </summary>
    [Fact]
    public async Task CreateAsyncShouldCreateEquipment()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<EquipmentCreateModel>();
        
        // Act
        var result = await equipmentService.CreateAsync(equipment, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(equipment);
    }
    
    /// <summary>
    /// EditAsync должен отредактировать оборудование
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldEditEquipment()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>(x =>
        {
            x.Name = "Барашек3000";
        });
        await Context.AddRangeAsync(equipment);
        await UnitOfWork.SaveChangesAsync();

        var editEquipment = TestEntityProvider.Shared.Create<EquipmentCreateModel>(x =>
        {
            x.Name = "Пряник домашний2000";
        });
        
        // Act
        var result = await equipmentService.EditAsync(equipment.Id, editEquipment, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(editEquipment);
    }
    
    /// <summary>
    /// EditAsync должен отредактировать оборудование
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldThrowExceptionByEquipmentId()
    {
        // Arrange
        var editEquipment = TestEntityProvider.Shared.Create<EquipmentCreateModel>(x =>
        {
            x.Name = "Пряник домашний2000";
        });
        
        // Act
        var result = () => equipmentService.EditAsync(Guid.NewGuid(), editEquipment, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// DeleteAsync должен удалить оборудование
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldDeleteEquipment()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        await Context.AddRangeAsync(equipment);
        await UnitOfWork.SaveChangesAsync();
        // Act
        await equipmentService.DeleteAsync(equipment.Id, CancellationToken.None);
        
        // Assert
        var result = Context.Set<Equipment>().Single(x => x.Id == equipment.Id);
        result.DeletedAt.Should().NotBeNull();
    }
    
    /// <summary>
    /// DeleteAsync должен выбросить ошибку
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldThrowException()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        
        // Act
        var result = () => equipmentService.DeleteAsync(equipmentId, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
}