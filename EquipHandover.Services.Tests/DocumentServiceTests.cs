using Ahatornn.TestGenerator;
using AutoMapper;
using EquipHandover.Common.Contracts;
using EquipHandover.Context.Tests;
using EquipHandover.Entities;
using EquipHandover.Repositories.ReadRepositories;
using EquipHandover.Repositories.WriteRepositories;
using EquipHandover.Services.AutoMappers;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Models.Document;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Moq;
using Xunit;

namespace EquipHandover.Services.Tests;

/// <summary>
/// Тесты на <see cref="DocumentService"/>
/// </summary>
public class DocumentServiceTests : EquipHandoverContextInMemory
{
    private readonly IDocumentService documentService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentServiceTests"/>
    /// </summary>
    public DocumentServiceTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });
        
        var mapper = config.CreateMapper();
        documentService = new DocumentService(mapper,
            new DocumentReadRepository(Context),
            new DocumentWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
            new EquipmentReadRepository(Context),
            new ReceiverReadRepository(Context),
            new SenderReadRepository(Context),
            new DocumentEquipmentReadRepository(Context),
            new DocumentEquipmentWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
            UnitOfWork
        );
    }

    /// <summary>
    /// GetAllAsync должен вернуть значения
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
        await Context.AddRangeAsync(document1, document2, document3, receiver, sender);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await documentService.GetAllAsync(CancellationToken.None);
        
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
        var result = await documentService.GetAllAsync(CancellationToken.None);
        
        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// CreateAsync должен создать документ
    /// </summary>
    [Fact]
    public async Task CreateAsyncShouldCreateDocument()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        await Context.AddRangeAsync(receiver, sender, equipment);
        await UnitOfWork.SaveChangesAsync();
        var document =  TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
        {
            x.SenderId = sender.Id;
            x.ReceiverId = receiver.Id;
            x.EquipmentIds = [equipment.Id];
        });
        
        // Act
        var result = await documentService.CreateAsync(document, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(document, DocumentCreateModelExcludings);
    }

    /// <summary>
    /// EditAsync должен отредактировать документ
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldEditDocument()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        var document = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.City = "Насрудин";
            x.SenderId =  sender.Id;
            x.ReceiverId = receiver.Id;
        });

        var documentEquipment = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.DocumentId = document.Id;
            x.EquipmentId =  equipment.Id;
        });
        await Context.AddRangeAsync(document, sender, documentEquipment, receiver, equipment);
        await UnitOfWork.SaveChangesAsync();

        var editDocument = TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
        {
            x.City = "Дагестан";
            x.SenderId = sender.Id;
            x.ReceiverId = receiver.Id;
            x.EquipmentIds = [equipment.Id];
        });
        
        // Act
        var result = await documentService.EditAsync(document.Id ,editDocument, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(editDocument, DocumentCreateModelExcludings);
    }

    private static EquivalencyAssertionOptions<DocumentCreateModel> DocumentCreateModelExcludings(
        EquivalencyAssertionOptions<DocumentCreateModel> opts) =>
        opts.Excluding(x => x.SenderId)
            .Excluding(x => x.EquipmentIds)
            .Excluding(x => x.ReceiverId);
}