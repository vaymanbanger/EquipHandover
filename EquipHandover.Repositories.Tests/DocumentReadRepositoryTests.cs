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
    /// GetByIdAsync должен быть null
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncAsyncShouldBeNull()
    {
        // Arrange
        var document1 = TestEntityProvider.Shared.Create<Entities.Document>();
        var document2 = TestEntityProvider.Shared.Create<Entities.Document>();
        var documentId = Guid.NewGuid();
        await Context.AddRangeAsync(document1, document2);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentReadRepository.GetByIdAsync(documentId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
    
    /// <summary>
    /// GetByIdAsync долежен быть null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncAsyncBeNullBySoftDelete()
    {
        // Arrange
        var document = TestEntityProvider.Shared.Create<Entities.Document>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddRangeAsync(document);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentReadRepository.GetByIdAsync(document.Id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
    
    /// <summary>
    /// GetByIdAsync должен вернуть значение
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncAsyncShouldReturnValue()
    {
        // Arrange
        var document = TestEntityProvider.Shared.Create<Entities.Document>();
        await Context.AddRangeAsync(document);
        await UnitOfWork.SaveChangesAsync();
        
        // Act
        var result =
            await documentReadRepository.GetByIdAsync(document.Id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(document);
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
        var document1 = TestEntityProvider.Shared.Create<Entities.Document>();
        var document2 = TestEntityProvider.Shared.Create<Entities.Document>(
            x => x.DeletedAt = DateTimeOffset.UtcNow);
        var document3 = TestEntityProvider.Shared.Create<Entities.Document>();
    
        await Context.AddRangeAsync(document1, document2, document3);
        await UnitOfWork.SaveChangesAsync();
    
        // Act
        var result = await documentReadRepository.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }
}