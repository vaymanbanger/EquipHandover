namespace EquipHandover.Entities;

/// <summary>
/// Сущность принимающего
/// </summary>
public class Receiver : BaseAuditEntity
{
    /// <summary>
    /// Полное имя принимающего
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Предприятие
    /// </summary>
    public string Enterprise { get; set; } = string.Empty;
    
    /// <summary>
    /// Основной государственный регистрационный номер
    /// </summary>
    public int RegistrationNumber { get; set; }
}