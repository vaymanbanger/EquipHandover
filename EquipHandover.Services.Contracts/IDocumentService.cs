using EquipHandover.Services.Contracts.Models.Document;

namespace EquipHandover.Services.Contracts;
/// <summary>
/// Интерфейс сервиса для работы с документом
/// </summary>
public interface IDocumentService
{
    /// <summary>
    /// Возвращает список <see cref="DocumentModel"/>
    /// </summary>
    Task<IReadOnlyCollection<DocumentModel>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавляет новый <see cref="DocumentModel"/>
    /// </summary>
    Task<DocumentModel> CreateAsync(DocumentCreateModel documentCreateModel, CancellationToken cancellationToken);
}