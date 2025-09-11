using System.Linq.Expressions;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Extensions;

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

    Task<IReadOnlyCollection<DocumentEquipment>> IDocumentEquipmentReadRepository.GetByEquipmentIdAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
        => reader.Read<DocumentEquipment>()
            .NotDeletedAt()
            .Where(ByEquipmentIds(ids))
            .ToReadOnlyCollectionAsync(cancellationToken);

    Task<IReadOnlyCollection<DocumentEquipment>> IDocumentEquipmentReadRepository.GetByDocumentIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<DocumentEquipment>()
            .NotDeletedAt()
            .Where(x => x.DocumentId == id)
            .ToReadOnlyCollectionAsync(cancellationToken);
    
    /// <summary>
    /// Метод для вычисления идентификатора оборудования
    /// </summary>
    private static Expression<Func<DocumentEquipment, bool>> ByEquipmentIds(IReadOnlyCollection<Guid> ids)
    {
        var quantity = ids.Count;
        return quantity switch
        {
            0 => x => false,
            1 => x => x.EquipmentId == ids.First(),
            _ => x => ids.Contains(x.EquipmentId)
        };
    }
}