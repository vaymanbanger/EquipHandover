namespace EquipHandover.Entities;

/// <summary>
/// Сущность принимающего
/// </summary>
public class Receiver : BaseAuditEntity
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Предприятие
    /// </summary>
    public string Enterprise { get; set; } = string.Empty;
    
    /// <summary>
    /// ОГРН
    /// </summary>
    public int Ogrn { get; set; }
}