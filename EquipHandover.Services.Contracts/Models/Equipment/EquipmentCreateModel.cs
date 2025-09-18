namespace EquipHandover.Services.Contracts.Models.Equipment;

/// <summary>
/// Модель создания/редактирования оборудования
/// </summary>
public class EquipmentCreateModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Год выпуска
    /// </summary>
    public int ManufacturedYear { get; set; }
    
    /// <summary>
    /// Заводской номер
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;
}