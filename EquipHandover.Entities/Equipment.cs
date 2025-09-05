using EquipHandover.Entities.Contracts;

namespace EquipHandover.Entities;
/// <summary>
/// Сущность оборудования
/// </summary>
public class Equipment : BaseAuditEntity
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
    
    /// <summary>
    /// Навигационное свойство списка <see cref="DocumentEquipment"/>
    /// </summary>
    public List<DocumentEquipment> DocumentEquipments { get; set; } = new List<DocumentEquipment>();
}