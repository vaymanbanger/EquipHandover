namespace EquipHandover.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.DocumentEquipment"/>
/// </summary>
public interface IDocumentEquipmentReadRepository
{
    
    /// <summary>
    /// Получает <see cref="Entities.DocumentEquipment"/> по идентификатору
    /// </summary>
    Task<IReadOnlyCollection<Entities.DocumentEquipment>> GetByEquipmentIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает <see cref="Entities.DocumentEquipment"/> по DocumentId
    /// </summary>
    Task<IReadOnlyCollection<Entities.DocumentEquipment>> GetByDocumentIdAsync(Guid id,CancellationToken cancellationToken);
    
}