namespace EquipHandover.Web.Models.Equipment;

/// <summary>
/// API модель для создания оборудования
/// </summary>
public class EquipmentRequestApiModel
{
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