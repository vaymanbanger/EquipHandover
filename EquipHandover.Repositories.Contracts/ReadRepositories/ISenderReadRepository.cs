namespace EquipHandover.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Sender"/>
/// </summary>
public interface ISenderReadRepository
{
    /// <summary>
    /// Получает <see cref="Entities.Sender"/> по идентификатору
    /// </summary>
    Task<Entities.Sender?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает коллекцию <see cref="Entities.Sender"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Sender>> GetAllAsync(CancellationToken cancellationToken);
}