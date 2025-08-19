namespace EquipHandover.Entities;
/// <summary>
/// Сущность документа
/// </summary>
public class Document : BaseAuditEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата аренды
    /// </summary>
    public DateOnly RentalDate { get; set; }
    
    /// <summary>
    /// Номер подписания
    /// </summary>
    public string SignatureNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Город
    /// </summary>
    public string City { get; set; } = string.Empty;
}
