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
/// Тесты сценариев <see cref="DocumentController"/>
/// </summary>
[Collection(nameof(EquipHandoverCollection))]
public class DocumentControllerTests
{
    private readonly IEquipHandoverApiClient webClient;
    private readonly EquipHandoverContext context;
    private readonly IUnitOfWork unitOfWork;
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentControllerTests"/>
    /// </summary>
    public DocumentControllerTests(EquipHandoverApiFixture fixture)
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
        var sender = TestEntityProvider.Shared.Create<Sender>(
            x => x.TaxPayerId = "023456789012");
        var receiver = TestEntityProvider.Shared.Create<Receiver>(
            x => x.RegistrationNumber = "0234567890123");
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        var document = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.RentalDate = new DateOnly(2015, 1, 1);
            x.SignatureNumber = new DateOnly(2025, 1, 1);
            x.SenderId =  sender.Id;
            x.ReceiverId = receiver.Id;
        });
        
        var sender1 = TestEntityProvider.Shared.Create<Sender>(
            x => x.TaxPayerId = "023456789013");
        var receiver1 = TestEntityProvider.Shared.Create<Receiver>(
            x => x.RegistrationNumber = "0234567890122");
        
        var document1 = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.RentalDate = new DateOnly(2024, 2, 3);
            x.SignatureNumber = new DateOnly(2025, 2, 1);
            x.SenderId =  sender1.Id;
            x.ReceiverId = receiver1.Id;
        });
        
        var sender2 = TestEntityProvider.Shared.Create<Sender>(
            x => x.TaxPayerId = "023456789023");
        var receiver2 = TestEntityProvider.Shared.Create<Receiver>(
            x => x.RegistrationNumber = "0234567890112");
        
        var document2 = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.RentalDate = new DateOnly(2024, 2, 3);
            x.SignatureNumber = new DateOnly(2025, 2, 1);
            x.SenderId =  sender2.Id;
            x.ReceiverId = receiver2.Id;
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        
        await context.AddRangeAsync(document, document1, document2, sender, sender2, sender1, receiver1, receiver2, receiver, equipment);
        await unitOfWork.SaveChangesAsync();
        
        // Act
        var response = await webClient.DocumentAllAsync();
        
        // Assert
        response.Should().ContainSingle(x => x.Id == document.Id)
            .And.HaveCount(2);
    }

    /// <summary>
    /// Create должен создать документ
    /// </summary>
    [Fact]
    public async Task CreateShouldCreateDocument()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>(
            x => x.TaxPayerId = "023426789012");
        var receiver = TestEntityProvider.Shared.Create<Receiver>(
            x => x.RegistrationNumber = "0214267890123");
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        
        await context.AddRangeAsync(sender, receiver, equipment);
        await unitOfWork.SaveChangesAsync();
        
        var document = TestEntityProvider.Shared.Create<DocumentRequestApiModel>(x =>
        {
            x.RentalDate = DateTimeOffset.UtcNow;
            x.SignatureNumber = DateTimeOffset.UtcNow;
            x.SenderId = sender.Id;
            x.ReceiverId = receiver.Id;
            x.City = "Буйнакск";
            x.EquipmentIds = [equipment.Id];
        });
        
        // Act
        var response = await webClient.DocumentPOSTAsync(document);
        
        // Assert
        response.Should().BeEquivalentTo(document);
    }
}