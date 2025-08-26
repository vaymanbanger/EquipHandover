namespace EquipHandover.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Receiver"/>
/// </summary>
public interface IReceiverReadRepository
{
    /// <summary>
    /// Получает <see cref="Entities.Receiver"/> по идентификатору
    /// </summary>
    Task<Entities.Receiver?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает коллекцию <see cref="Entities.Receiver"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Receiver>> GetAllAsync(CancellationToken cancellationToken);
}