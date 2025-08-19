namespace EquipHandover.Entities;
/// <summary>
/// Сущность документа
/// </summary>
public class Document : BaseAuditEntity
{
    /// <summary>
    /// Идентификатор документа
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
    
    /// <summary>
    /// Идентификатор <see cref="Sender"/>
    /// </summary>
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство <see cref="Sender"/>
    /// </summary>
    public Sender Sender { get; set; }
    
    /// <summary>
    /// Идентификатор <see cref="Receiver"/>
    /// </summary>
    public Guid ReceiverId { get; set; }
    
    /// <summary>
    /// Навигационное свойство <see cref="Receiver"/>
    /// </summary>
    public Receiver Receiver { get; set; }
    
    /// <summary>
    /// Навигационное свойство списка <see cref="Equipment"/>
    /// </summary>
    public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
