using EquipHandover.Services.Contracts.Models.Document;

namespace EquipHandover.Services.Contracts;
/// <summary>
/// Интерфейс сервиса для работы с документом
/// </summary>
public interface IDocumentService
{
    /// <summary>
    /// Возвращает <see cref="DocumentModel"/> по идентификатору
    /// </summary>
    Task<DocumentModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Возвращает список <see cref="DocumentModel"/>
    /// </summary>
    Task<IReadOnlyCollection<DocumentModel>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавляет новый <see cref="DocumentModel"/>
    /// </summary>
    Task<DocumentModel> CreateAsync(DocumentCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Редактирует существующий <see cref="DocumentModel"/>
    /// </summary>
    Task<DocumentModel> EditAsync(Guid id, DocumentCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет существующий <see cref="DocumentModel"/>
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Экспортирует <see cref="DocumentModel"/> по идентификатору
    /// </summary> 
    Task<Stream> ExportByIdAsync(Guid id, CancellationToken cancellationToken);
}