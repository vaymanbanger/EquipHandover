using EquipHandover.Context.Tests;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;
using Ahatornn.TestGenerator;
using EquipHandover.Entities;

namespace EquipHandover.Repositories.Tests;

/// <summary>
/// Тесты на <see cref="DocumentEquipmentReadRepository"/>
/// </summary>
public class DocumentEquipmentReadRepositoryTests : EquipHandoverContextInMemory
{
    private readonly IDocumentEquipmentReadRepository documentEquipmentReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentEquipmentReadRepositoryTests"/>
    /// </summary>
    public DocumentEquipmentReadRepositoryTests()
    {
        documentEquipmentReadRepository = new DocumentEquipmentReadRepository(Context);
    }
    
    /// <summary>
    /// GetByDocumentIdAsync должен быть пустым
    /// </summary>
    [Fact]
    public async Task GetByDocumentIdAsyncShouldBeEmpty()
    {
        // Arrange
        var documentsEquipment1 = TestEntityProvider.Shared.Create<DocumentEquipment>();
        var documentsEquipment2 = TestEntityProvider.Shared.Create<DocumentEquipment>();
        var documentId = Guid.NewGuid();
        Context.AddRange(documentsEquipment1, documentsEquipment2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentEquipmentReadRepository.GetByDocumentIdAsync(documentId, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
    
    /// <summary>
    /// GetByDocumentIdAsync должен быть пустым при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByDocumentIdAsyncShouldBeEmptyBySoftDelete()
    {
        // Arrange
        var documentsEquipment = TestEntityProvider.Shared.Create<DocumentEquipment>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(documentsEquipment);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentEquipmentReadRepository.GetByDocumentIdAsync(documentsEquipment.Id, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// GetByDocumentIdAsync должен вернуть значения
    /// </summary>
    [Fact]
    public async Task GetByDocumentIdAsyncShouldReturnValues()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var documentsEquipment1 = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.DocumentId = documentId;
        });
        var documentsEquipment2 = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.DocumentId = documentId;
        }); 
        var documentsEquipment3 = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.DocumentId = documentId;
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        Context.AddRange(documentsEquipment1, documentsEquipment2, documentsEquipment3);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result =
            await documentEquipmentReadRepository.GetByDocumentIdAsync(documentId, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }

    /// <summary>
    /// GetByEquipmentIdsAsync должен вернуть null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByEquipmentIdsAsyncShouldReturnNullBySoftDelete()
    {
        // Arrange
        var documentsEquipment = TestEntityProvider.Shared.Create<DocumentEquipment>(
                x => x.DeletedAt = DateTimeOffset.UtcNow);
        Context.AddRange(documentsEquipment);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await documentEquipmentReadRepository.GetByEquipmentIdAsync(
            new List<Guid> { documentsEquipment.Id }, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
    
    /// <summary>
    /// GetByEquipmentIdsAsync должен быть пустым
    /// </summary>
    [Fact]
    public async Task GetByEquipmentIdsAsyncShouldBeEmpty()
    {
        // Arrange
        var documentsEquipment1 = TestEntityProvider.Shared.Create<DocumentEquipment>();
        var documentsEquipment2 = TestEntityProvider.Shared.Create<DocumentEquipment>();
        var equipmentIds = Guid.NewGuid();
        Context.AddRange(documentsEquipment1, documentsEquipment2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentEquipmentReadRepository.GetByEquipmentIdAsync(
                new List<Guid>{ equipmentIds }, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
    
    /// <summary>
    /// GetByEquipmentIdsAsync должен вернуть значения
    /// </summary>
    [Fact]
    public async Task GetByEquipmentIdsAsyncShouldReturnValues()
    {
        // Arrange
        var equipmentIds = Guid.NewGuid();
        var documentsEquipment1 = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.EquipmentId = equipmentIds;
        });
        var documentsEquipment2 = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.EquipmentId = equipmentIds;
        }); 
        var documentsEquipment3 = TestEntityProvider.Shared.Create<DocumentEquipment>(x =>
        {
            x.EquipmentId = equipmentIds;
            x.DeletedAt = DateTimeOffset.UtcNow;
        });
        Context.AddRange(documentsEquipment1, documentsEquipment2, documentsEquipment3);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result =
            await documentEquipmentReadRepository.GetByEquipmentIdAsync(
                new List<Guid> { equipmentIds }, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }
}