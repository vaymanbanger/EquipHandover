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
/// Тесты сценариев <see cref="ReceiverController"/>
/// </summary>
[Collection(nameof(EquipHandoverCollection))]
public class ReceiverControllerTests
{
    private readonly IEquipHandoverApiClient webClient;
    private readonly EquipHandoverContext context;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverControllerTests"/>
    /// </summary>
    public ReceiverControllerTests(EquipHandoverApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
        unitOfWork = fixture.UnitOfWork;
    }

    /// <summary>
    /// GetAll должен вернуть не пустую коллекцию
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValue()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>(
            x => x.RegistrationNumber = "1214227890123");
        var receiver1= TestEntityProvider.Shared.Create<Receiver>(
            x => x.RegistrationNumber = "2214227890123");
        var receiver2 = TestEntityProvider.Shared.Create<Receiver>(x =>
        {
            x.RegistrationNumber = "3214227890123";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        await context.AddRangeAsync(receiver, receiver1, receiver2);
        await unitOfWork.SaveChangesAsync();
        
        // Act
        var result = await webClient.ReceiverAllAsync();
        
        // Assert
        result.Should().ContainSingle(x => x.Id == receiver.Id)
            .And.ContainSingle(x => x.Id == receiver1.Id);
        var newValue = context.Set<Receiver>().Single(x => x.Id == receiver2.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Create должен создать принимающего
    /// </summary>
    [Fact]
    public async Task CreateShouldCreateReceiver()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<ReceiverRequestApiModel>(
            x => x.RegistrationNumber = "1215223890123");
        
        // Act
        var result = await webClient.ReceiverPOSTAsync(receiver);

        // Assert
        result.Should().BeEquivalentTo(receiver);
    }

    /// <summary>
    /// Edit должен отредактировать принимающего
    /// </summary>
    [Fact]
    public async Task EditShouldEditReceiver()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>(x =>
        {
            x.FullName = "Хачевский Вадим Викторович";
            x.RegistrationNumber = "1215227890123";
        });
        await context.AddRangeAsync(receiver);
        await unitOfWork.SaveChangesAsync();

        var editReceiver = TestEntityProvider.Shared.Create<ReceiverRequestApiModel>(x =>
        {
            x.FullName = "Налакомилсявский Виктор Алексеевич";
            x.RegistrationNumber = "1215227890123";
        });
        
        // Act
        var result = await webClient.ReceiverPUTAsync(receiver.Id, editReceiver);
        
        // Assert
        result.Should().BeEquivalentTo(editReceiver);
    }

    /// <summary>
    /// Delete должен удалить принимающего
    /// </summary>
    [Fact]
    public async Task DeleteShouldDeleteReceiver()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>(
            x => x.RegistrationNumber = "1215227290123");
        await context.AddRangeAsync(receiver);
        await unitOfWork.SaveChangesAsync();
        
        // Act
        await webClient.ReceiverDELETEAsync(receiver.Id);
        
        // Assert
        await unitOfWork.SaveChangesAsync();
        var newValue = context.Set<Receiver>().Single(x => x.Id == receiver.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}