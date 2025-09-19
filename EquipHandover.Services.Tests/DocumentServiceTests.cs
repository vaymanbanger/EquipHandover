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
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Export;
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
            UnitOfWork, 
            new ExcelService()
        );
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id документа с мягким удалением
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionBySoftDelete()
    {
        // Arrange
        var document = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        await Context.AddRangeAsync(document);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => documentService.GetByIdAsync(document.Id, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// GetByIdAsync должен выбросить ошибку о несуществующем Id документа
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncShouldThrowExceptionByDocumentId()
    {
        // Arrange
        var document = TestEntityProvider.Shared.Create<Document>();
        await Context.AddRangeAsync(document);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result= () => documentService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        
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
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var document = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.Sender = sender;
            x.Receiver = receiver;
        });
        await Context.AddRangeAsync(document, sender, receiver);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result = await documentService.GetByIdAsync(document.Id, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(document, options => options.ExcludingMissingMembers());
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
    /// CreateAsync должен выбросить ошибку о несуществующем Id принимающего
    /// </summary>
    [Fact]
    public async Task CreateAsyncShouldThrowExceptionByReceiverId()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        await Context.AddRangeAsync(sender, equipment);
        await UnitOfWork.SaveChangesAsync();
        var document =  TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
        {
            x.SenderId = sender.Id;
            x.ReceiverId = Guid.NewGuid();
            x.EquipmentIds = [equipment.Id];
        });
        
        // Act
        var result = () => documentService.CreateAsync(document, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// CreateAsync должен выбросить ошибку о несуществующем Id отправителя
    /// </summary>
    [Fact]
    public async Task CreateAsyncShouldThrowExceptionBySenderId()
    {
        // Arrange
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var equipment = TestEntityProvider.Shared.Create<Equipment>();
        await Context.AddRangeAsync(receiver, equipment);
        await UnitOfWork.SaveChangesAsync();
        var document =  TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
        {
            x.SenderId = Guid.NewGuid();
            x.ReceiverId = receiver.Id;
            x.EquipmentIds = [equipment.Id];
        });
        
        // Act
        var result = () => documentService.CreateAsync(document, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// CreateAsync должен выбросить ошибку о несуществующем Ids оборудования
    /// </summary>
    [Fact]
    public async Task CreateAsyncShouldThrowExceptionByEquipmentIds()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        await Context.AddRangeAsync(receiver, sender);
        await UnitOfWork.SaveChangesAsync();
        var document =  TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
        {
            x.SenderId = sender.Id;
            x.ReceiverId = receiver.Id;
            x.EquipmentIds = [Guid.NewGuid()];
        });
        
        // Act
        var result = () => documentService.CreateAsync(document, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
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

    /// <summary>
    /// EditAsync должен выбросить ошибку о несуществующем Id документа
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldThrowExceptionByDocumentId()
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
        var result = () => documentService.EditAsync(Guid.NewGuid(),editDocument, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// EditAsync должен выбросить ошибку о несуществующем Id принимающего
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldThrowExceptionByReceiverId()
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
            x.ReceiverId = Guid.NewGuid();
            x.EquipmentIds = [equipment.Id];
        });
        
        // Act
        var result = () => documentService.EditAsync(document.Id, editDocument, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    /// <summary>
    /// EditAsync должен выбросить ошибку о несуществующем Id отправителя
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldThrowExceptionBySenderId()
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
            x.SenderId = Guid.NewGuid();
            x.ReceiverId = receiver.Id;
            x.EquipmentIds = [equipment.Id];
        });
        
        // Act
        var result = () => documentService.EditAsync(document.Id, editDocument, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }

    
    /// <summary>
    /// EditAsync должен выбросить ошибку о несуществующем Ids оборудования
    /// </summary>
    [Fact]
    public async Task EditAsyncShouldThrowExceptionByEquipmentIds()
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
            x.SenderId = Guid.NewGuid();
            x.ReceiverId = receiver.Id;
            x.EquipmentIds = [Guid.NewGuid()];
        });
        
        // Act
        var result = () => documentService.EditAsync(document.Id, editDocument, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }

    /// <summary>
    /// DeleteAsync должен удалить документ
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldDeleteDocument()
    {
        // Arrange
        var sender = TestEntityProvider.Shared.Create<Sender>();
        var receiver = TestEntityProvider.Shared.Create<Receiver>();
        var document = TestEntityProvider.Shared.Create<Document>(x =>
        {
            x.ReceiverId = receiver.Id;
            x.SenderId = sender.Id;
        });
        await Context.AddRangeAsync(document, sender, receiver);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        await documentService.DeleteAsync(document.Id, CancellationToken.None);
        
        // Assert
        var result = Context.Set<Document>().Single(x => x.Id == document.Id);
        result.DeletedAt.Should().NotBeNull();
    }
    
    /// <summary>
    /// DeleteAsync должен выбросить ошибку
    /// </summary>
    [Fact]
    public async Task DeleteAsyncShouldThrowException()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        
        // Act
        var result = () => documentService.DeleteAsync(documentId, CancellationToken.None);
        
        // Assert
        await result.Should().ThrowAsync<EquipHandoverNotFoundException>();
    }
    
    private static EquivalencyAssertionOptions<DocumentCreateModel> DocumentCreateModelExcludings(
        EquivalencyAssertionOptions<DocumentCreateModel> opts) =>
        opts.Excluding(x => x.SenderId)
            .Excluding(x => x.EquipmentIds)
            .Excluding(x => x.ReceiverId);
}