using EquipHandover.Web.Models.Equipment;
using EquipHandover.Web.Models.Receiver;
using EquipHandover.Web.Models.Sender;

namespace EquipHandover.Web.Models.Document;

/// <summary>
/// API модель документа
/// </summary>
public class DocumentResponseApiModel : DocumentBaseApiModel
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Sender"/>
    /// </summary>
    public SenderResponseApiModel Sender { get; set; } = new SenderResponseApiModel();

    /// <summary>
    /// Навигационное свойство <see cref="Receiver"/>
    /// </summary>
    public ReceiverResponseApiModel Receiver { get; set; } = new ReceiverResponseApiModel();
    
    /// <summary>
    /// Навигационное свойство списка <see cref="Equipment"/>
    /// </summary>
    public List<EquipmentResponseApiModel> Equipment { get; set; } = new List<EquipmentResponseApiModel>();
}