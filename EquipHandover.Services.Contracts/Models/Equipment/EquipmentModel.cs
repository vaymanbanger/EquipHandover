namespace EquipHandover.Services.Contracts.Models.Equipment;

/// <summary>
/// Модель оборудования
/// </summary>
public class EquipmentModel : EquipmentCreateModel
{
    /// <summary>
    /// Идентификатор оборудования
    /// </summary>
    public Guid Id { get; set; }
}