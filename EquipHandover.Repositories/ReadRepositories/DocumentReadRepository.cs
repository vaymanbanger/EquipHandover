using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Repositories.ReadRepositories;

/// <inheritdoc cref="IDocumentReadRepository"/>
public class DocumentReadRepository : IDocumentReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentReadRepository"/>
    /// </summary>
    public DocumentReadRepository(IReader reader)
    {
        this.reader = reader;
    }
    
    Task<Document?> IDocumentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Document>> IDocumentReadRepository.GetAllAsync(CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .ToReadOnlyCollectionAsync(cancellationToken);
}