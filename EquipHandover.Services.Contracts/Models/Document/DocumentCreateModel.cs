using EquipHandover.Entities;

namespace EquipHandover.Services.Contracts.Models.Document;

/// <summary>
/// Модель создания/редактирования документа
/// </summary>
public class DocumentCreateModel : DocumentBaseModel
{
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