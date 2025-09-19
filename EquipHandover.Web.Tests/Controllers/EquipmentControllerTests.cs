using Ahatornn.TestGenerator;
using EquipHandover.Context;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Web.Controllers;
using EquipHandover.Web.Tests.Client;
using EquipHandover.Web.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace EquipHandover.Web.Tests.Controllers;

/// <summary>
/// Тесты сценариев <see cref="EquipmentController"/>
/// </summary>
[Collection(nameof(EquipHandoverCollection))]
public class EquipmentControllerTests
{
    private readonly IEquipHandoverApiClient webClient;
    private readonly EquipHandoverContext context;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentControllerTests"/>
    /// </summary>
    public EquipmentControllerTests(EquipHandoverApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
        unitOfWork = fixture.UnitOfWork;
    }

    /// <summary>
    /// GetById должен получить оборудование по идентификатору
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        await context.AddRangeAsync(equipment);
        await unitOfWork.SaveChangesAsync();

        // Act
        var response = await webClient.EquipmentGETAsync(equipment.Id);

        // Assert
        response.Should().BeEquivalentTo(equipment, options => options.ExcludingMissingMembers());
    }

    /// <summary>
    /// GetAll должен вернуть не пустую коллекцию
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValue()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        var equipment1= TestEntityProvider.Shared.Create<Equipment>();
        var equipment2 = TestEntityProvider.Shared.Create<Equipment>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        await context.AddRangeAsync(equipment, equipment1, equipment2);
        await unitOfWork.SaveChangesAsync();
        
        // Act
        var response = await webClient.EquipmentAllAsync();
        
        // Assert
        response.Should().ContainSingle(x => x.Id == equipment.Id)
            .And.ContainSingle(x => x.Id == equipment1.Id);
        var newValue = context.Set<Equipment>().Single(x => x.Id == equipment2.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Create должен создать оборудование
    /// </summary>
    [Fact]
    public async Task CreateShouldCreateEquipment()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<EquipmentCreateApiModel>(
            x => x.ManufacturedYear = 2024);
        
        // Act
        var result = await webClient.EquipmentPOSTAsync(equipment);
        
        // Assert
        result.Should().BeEquivalentTo(equipment);
    }

    /// <summary>
    /// Edit должен отредактировать документ
    /// </summary>
    [Fact]
    public async Task EditShouldEditEquipment()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>(x =>
        {
            x.Name = "Барашек3000";
            x.ManufacturedYear = 2025;
        });
        await context.AddRangeAsync(equipment);
        await unitOfWork.SaveChangesAsync();

        var editEquipment = TestEntityProvider.Shared.Create<EquipmentCreateApiModel>(x =>
        {
            x.Name = "Пряник домашний2000";
            x.ManufacturedYear = 2020;
        });
        
        // Act
        var result = await webClient.EquipmentPUTAsync(equipment.Id, editEquipment, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(editEquipment);
    }

    /// <summary>
    /// Delete должен удалить оборудование
    /// </summary>
    [Fact]
    public async Task DeleteShouldDeleteEquipment()
    {
        // Arrange
        var equipment = TestEntityProvider.Shared.Create<Equipment>(
            x => x.ManufacturedYear = 2020);
        await context.AddRangeAsync(equipment);
        await unitOfWork.SaveChangesAsync();
        
        // Act
        await webClient.EquipmentDELETEAsync(equipment.Id);
        
        // Assert
        await unitOfWork.SaveChangesAsync();
        var newValue = context.Set<Equipment>().Single(x => x.Id == equipment.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}