using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Repositories.ReadRepositories;

/// <inheritdoc cref="IDocumentEquipmentReadRepository"/>
public class DocumentEquipmentReadRepository : IDocumentEquipmentReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentEquipmentReadRepository"/>
    /// </summary>
    public DocumentEquipmentReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<DocumentEquipment?> IDocumentEquipmentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<DocumentEquipment>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<DocumentEquipment>> IDocumentEquipmentReadRepository.GetByIdsAsync(IReadOnlyCollection<Guid> id, CancellationToken cancellationToken)
        => reader.Read<DocumentEquipment>()
            .NotDeletedAt()
            .ByIds(id)
            .ToReadOnlyCollectionAsync(cancellationToken);

    Task<IReadOnlyCollection<DocumentEquipment>> IDocumentEquipmentReadRepository.GetByDocumentIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<DocumentEquipment>()
            .NotDeletedAt()
            .Where(x => x.DocumentId == id)
            .ToReadOnlyCollectionAsync(cancellationToken);
}