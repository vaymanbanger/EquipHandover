using EquipHandover.Services.Contracts.Models.Document;

namespace EquipHandover.Services.Contracts.Export;

/// <summary>
/// Интерфейс сервиса для экспорта документа в Excel
/// </summary>
public interface IExcelService
{
    /// <summary>
    /// Экспортирует <see cref="DocumentModel"/> в Excel
    /// </summary>
    byte[] Export(DocumentModel document, CancellationToken cancellationToken);
}