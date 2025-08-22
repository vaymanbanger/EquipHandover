using EquipHandover.Services.Contracts.Models;
using EquipHandover.Services.Contracts.Models.Equipment;

namespace EquipHandover.Services.Contracts;

public interface IEquipmentService
{
    /// <summary>
    /// Возвращает список <see cref="EquipmentModel"/>
    /// </summary>
    Task<IReadOnlyCollection<EquipmentModel>> GetAllAsync(CancellationToken cancellationToken);
}