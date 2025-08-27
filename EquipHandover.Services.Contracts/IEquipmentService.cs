using EquipHandover.Services.Contracts.Models.Equipment;

namespace EquipHandover.Services.Contracts;

/// <summary>
/// Интерфейс сервиса для работы с оборудованием
/// </summary>
public interface IEquipmentService
{
    /// <summary>
    /// Возвращает список <see cref="EquipmentModel"/>
    /// </summary>
    Task<IReadOnlyCollection<EquipmentModel>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Возвращает <see cref="EquipmentModel"/> по идентификатору
    /// </summary>
    Task<EquipmentModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавляет новый <see cref="EquipmentModel"/>
    /// </summary>
    Task<EquipmentModel> CreateAsync(EquipmentCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Редактирует существующий <see cref="EquipmentModel"/>
    /// </summary>
    Task<EquipmentModel> EditAsync(Guid id, EquipmentCreateModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет существующий <see cref="EquipmentModel"/>
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}