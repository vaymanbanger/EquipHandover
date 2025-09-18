using Ahatornn.TestGenerator;
using EquipHandover.Context.Tests;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace EquipHandover.Repositories.Tests;

/// <summary>
/// Тесты на <see cref="DocumentReadRepository"/>
/// </summary>
public class DocumentReadRepositoryTests : EquipHandoverContextInMemory
{
    private readonly IDocumentReadRepository documentReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentReadRepositoryTests"/>
    /// </summary>
    public DocumentReadRepositoryTests()
    {
        documentReadRepository = new DocumentReadRepository(Context);
    }
    
    /// <summary>
    /// GetAllAsync должен вернуть пустой список
    /// </summary>
    [Fact]
    public async Task GetAllAsyncShouldBeEmpty()
    {
        // Act
        var result =
            await documentReadRepository.GetAllAsync(CancellationToken.None);

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
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var document1 = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.Sender = sender;
            x.Receiver = receiver;
        });
        var document2 = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.Sender = sender;
            x.Receiver = receiver;
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        var document3 = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.Sender = sender;
            x.Receiver = receiver;
        });
        Context.AddRange(document1, document2, document3, receiver, sender);
        await UnitOfWork.SaveChangesAsync();
    
        // Act
        var result = await documentReadRepository.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }
    
    /// <summary>
    /// GetByIdWithFullModelAsync должен быть null
    /// </summary>
    [Fact]
    public async Task GetByIdWithFullModelAsyncShouldBeNull()
    {
        // Arrange
        var document1 = TestEntityProvider.Shared.Create<Document>();
        var document2 = TestEntityProvider.Shared.Create<Document>();
        var documentId = Guid.NewGuid();
        Context.AddRange(document1, document2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentReadRepository.GetByIdAsync(documentId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
    
    /// <summary>
    /// GetByIdWithFullModelAsync должен быть null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdWithFullModelAsyncShouldBeNullBySoftDelete()
    {
        // Arrange
        var document = TestEntityProvider.Shared.Create<Document>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(document);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentReadRepository.GetByIdAsync(document.Id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
    
    /// <summary>
    /// GetByIdWithFullModelAsync должен вернуть значение
    /// </summary>
    [Fact]
    public async Task GetByIdWithFullModelAsyncShouldReturnValue()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        var document = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.SenderId = sender.Id;
            x.Sender = sender;
            x.ReceiverId = receiver.Id;
            x.Receiver = receiver;
        });
        var documentEquipment = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.DocumentId = document.Id;
            x.EquipmentId =  equipment.Id;
        });
        Context.AddRange(receiver, sender, equipment, documentEquipment, document);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentReadRepository.GetByIdAsync(document.Id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(document, 
            options => options.ExcludingMissingMembers());
    }
}