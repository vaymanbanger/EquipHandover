namespace EquipHandover.Web.Models.Document;

/// <summary>
/// API модель для создания/редактирования документа
/// </summary>
public class DocumentRequestApiModel
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
    /// Идентификатор <see cref="Receiver"/>
    /// </summary>
    public Guid ReceiverId { get; set; }
    
    /// <summary>
    /// Идентификаторы <see cref="Equipment"/>
    /// </summary>
    public List<Guid> EquipmentIds { get; set; } = new List<Guid>();
}