namespace EquipHandover.Services.Contracts.Models.Equipment;

/// <summary>
/// Модель оборудования
/// </summary>
public class EquipmentModel
{
    /// <summary>
    /// Идентификатор оборудования
    /// </summary>
    public Guid Id{ get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Год выпуска
    /// </summary>
    public int ManufactureDate { get; set; }
    
    /// <summary>
    /// Заводской номер
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Номер оборудования
    /// </summary>
    public string EquipmentNumber { get; set; } = string.Empty;
    
}