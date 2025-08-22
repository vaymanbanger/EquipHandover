namespace EquipHandover.Entities;
/// <summary>
/// Сущность документа
/// </summary>
public class Document : BaseAuditEntity
{
    /// <summary>
    /// Дата аренды оборудования
    /// </summary>
    public DateOnly RentalDate { get; set; }
    
    /// <summary>
    /// Номер подписания договора
    /// </summary>
    public DateOnly SignatureNumber { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public string City { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор <see cref="Sender"/>
    /// </summary>
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство <see cref="Sender"/>
    /// </summary>
    public Sender? Sender { get; set; }
    
    /// <summary>
    /// Идентификатор <see cref="Receiver"/>
    /// </summary>
    public Guid ReceiverId { get; set; }
    
    /// <summary>
    /// Навигационное свойство <see cref="Receiver"/>
    /// </summary>
    public Receiver? Receiver { get; set; } 
    
    /// <summary>
    /// Навигационное свойство списка <see cref="DocumentEquipment"/>
    /// </summary>
    public List<DocumentEquipment> DocumentEquipments { get; set; } = new List<DocumentEquipment>();
}
