using System.Reflection.Metadata;

namespace EquipHandover.Services.Contracts.Export;

/// <summary>
/// Интерфейс сервиса для экспорта документа
/// </summary>
public interface IExportService
{
    /// <summary>
    /// Экспортирует <see cref="Document"/> по идентификатору
    /// </summary> 
    Task<Stream> ExportByIdAsync(Guid id, CancellationToken cancellationToken);
}