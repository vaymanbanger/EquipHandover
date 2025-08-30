using EquipHandover.Services.Contracts.Models.Sender;

namespace EquipHandover.Services.Contracts;

/// <summary>
/// Интерфейс сервиса для работы с отправителем
/// </summary>
public interface ISenderService
{
    /// <summary>
    /// Возвращает список <see cref="SenderModel"/>
    /// </summary>
    Task<IReadOnlyCollection<SenderModel>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавляет новый <see cref="SenderModel"/>
    /// </summary>
    Task<SenderModel> CreateAsync(SenderCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Редактирует существующий <see cref="SenderModel"/>
    /// </summary>
    Task<SenderModel> EditAsync(Guid id, SenderCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет существующий <see cref="SenderModel"/>
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}