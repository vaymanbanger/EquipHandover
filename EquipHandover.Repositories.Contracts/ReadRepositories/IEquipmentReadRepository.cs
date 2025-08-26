namespace EquipHandover.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Equipment"/>
/// </summary>
public interface IEquipmentReadRepository
{
    /// <summary>
    /// Получает <see cref="Entities.Equipment"/> по идентификатору
    /// </summary>
    Task<Entities.Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает коллекцию <see cref="Entities.Equipment"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Equipment>> GetAllAsync(CancellationToken cancellationToken);
}