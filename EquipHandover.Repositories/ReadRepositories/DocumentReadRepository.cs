using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Repositories.ReadRepositories;

/// <inheritdoc cref="IDocumentReadRepository"/>
public class DocumentReadRepository : IDocumentReadRepository, IRepositoryAnchor
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

    Task<IReadOnlyCollection<Document>> IDocumentReadRepository.GetByIdsAsync(IReadOnlyCollection<Guid> id, CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .ByIds(id)
            .ToReadOnlyCollectionAsync(cancellationToken);

    Task<IReadOnlyCollection<Document>> IDocumentReadRepository.GetAllAsync(CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .ToReadOnlyCollectionAsync(cancellationToken);
}