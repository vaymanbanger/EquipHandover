namespace EquipHandover.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Document"/>
/// </summary>
public interface IDocumentReadRepository
{
    /// <summary>
    /// Получает <see cref="Entities.Document"/> по идентификатору
    /// </summary>
    Task<Entities.Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает коллекцию <see cref="Entities.Document"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Document>> GetAllAsync(CancellationToken cancellationToken);
}