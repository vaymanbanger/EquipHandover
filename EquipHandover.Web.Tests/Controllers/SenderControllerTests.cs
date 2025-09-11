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
/// Тесты сценариев <see cref="SenderController"/>
/// </summary>
[Collection(nameof(EquipHandoverCollection))]
public class SenderControllerTests
{
    private readonly IEquipHandoverApiClient webClient;
    private readonly EquipHandoverContext context;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderControllerTests"/>
    /// </summary>
    public SenderControllerTests(EquipHandoverApiFixture fixture)
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
            x => x.TaxPayerId = "1454261890");
        var sender1= TestEntityProvider.Shared.Create<Sender>(
            x => x.TaxPayerId = "3234261890");
        var sender2 = TestEntityProvider.Shared.Create<Sender>(x =>
        {
            x.TaxPayerId = "0234261110";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        await context.AddRangeAsync(sender, sender1, sender2);
        await unitOfWork.SaveChangesAsync();
        
        // Act
        var result = await webClient.SenderAllAsync();
        
        // Assert
        result.Should().ContainSingle(x => x.Id == sender.Id)
            .And.ContainSingle(x => x.Id == sender1.Id);
        var newValue = context.Set<Sender>().Single(x => x.Id == sender2.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
    
    /// <summary>
    /// Create должен создать отправителя
    /// </summary>
    [Fact]
    public async Task CreateShouldCreateSender()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<SenderRequestApiModel>(
            x => x.TaxPayerId = "0234561890");
        
        // Act
        var result = await webClient.SenderPOSTAsync(sender);

        // Assert
        result.Should().BeEquivalentTo(sender);
    }
    
    /// <summary>
    /// Edit должен отредактировать отправителя
    /// </summary>
    [Fact]
    public async Task EditShouldEditSender()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>(x =>
        {
            x.FullName = "Хачевский Вадим Викторович";
            x.TaxPayerId = "0254261890";
        });
        await context.AddRangeAsync(sender);
        await unitOfWork.SaveChangesAsync();

        var editSender = TestEntityProvider.Shared.Create<SenderRequestApiModel>(x =>
        {
            x.FullName = "Налакомилсявский Виктор Алексеевич";
            x.TaxPayerId = "0232221890";
        });
        
        // Act
        var result = await webClient.SenderPUTAsync(sender.Id, editSender);
        
        // Assert
        result.Should().BeEquivalentTo(editSender);
    }

    /// <summary>
    /// Delete должен удалить отправителя
    /// </summary>
    [Fact]
    public async Task DeleteShouldDeleteSender()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>(
            x => x.TaxPayerId = "143426789012");
        await context.AddRangeAsync(sender);
        await unitOfWork.SaveChangesAsync();
        
        // Act
        await webClient.SenderDELETEAsync(sender.Id);
        
        // Assert
        await unitOfWork.SaveChangesAsync();
        var newValue = context.Set<Sender>().Single(x => x.Id == sender.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}