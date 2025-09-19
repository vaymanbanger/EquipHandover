using EquipHandover.Repositories.Contracts.Models;

namespace EquipHandover.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Document"/>
/// </summary>
public interface IDocumentReadRepository
{
    /// <summary>
    /// Получает коллекцию <see cref="Entities.Document"/>
    /// </summary>
    Task<IReadOnlyCollection<DocumentDbModel>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает <see cref="Entities.Document"/> по DocumentId с оборудованием
    /// </summary>
    Task<DocumentDbModel?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
}