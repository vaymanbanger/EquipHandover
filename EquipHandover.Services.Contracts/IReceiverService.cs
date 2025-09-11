using EquipHandover.Services.Contracts.Models.Receiver;

namespace EquipHandover.Services.Contracts;

/// <summary>
/// Интерфейс сервиса для работы с принимающим
/// </summary>
public interface IReceiverService
{
    /// <summary>
    /// Возвращает список <see cref="ReceiverModel"/>
    /// </summary>
    Task<IReadOnlyCollection<ReceiverModel>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавляет новый <see cref="ReceiverModel"/>
    /// </summary>
    Task<ReceiverModel> CreateAsync(ReceiverCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Редактирует существующий <see cref="ReceiverModel"/>
    /// </summary>
    Task<ReceiverModel> EditAsync(Guid id, ReceiverCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет существующий <see cref="ReceiverModel"/>
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}