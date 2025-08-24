using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Services.Contracts.Models.Receiver;
using EquipHandover.Services.Contracts.Models.Sender;

namespace EquipHandover.Services.Contracts.Models.Document;
/// <summary>
/// Модель документа
/// </summary>
public class DocumentModel
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public Guid Id { get; set; }
    
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
    /// Навигационное свойство <see cref="Sender"/>
    /// </summary>
    public SenderModel Sender { get; set; } = new SenderModel();

    /// <summary>
    /// Навигационное свойство <see cref="Receiver"/>
    /// </summary>
    public ReceiverModel Receiver { get; set; } = new ReceiverModel();
    
    /// <summary>
    /// Навигационное свойство списка <see cref="Equipment"/>
    /// </summary>
    public List<EquipmentModel> Equipment { get; set; } = new List<EquipmentModel>();
}