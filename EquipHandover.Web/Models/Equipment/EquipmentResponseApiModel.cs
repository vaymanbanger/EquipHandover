namespace EquipHandover.Web.Models.Equipment;

/// <summary>
/// API модель оборудования
/// </summary>
public class EquipmentResponseApiModel
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