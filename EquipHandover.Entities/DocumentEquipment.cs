namespace EquipHandover.Entities;
/// <summary>
/// Промежуточная сущность между документом и оборудованием
/// </summary>
public class DocumentEquipment : BaseAuditEntity
{
    /// <summary>
    /// Идентификатор документа
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Document"/>
    /// </summary>
    public Document? Document { get; set; }

    /// <summary>
    /// Идентификатор оборудования
    /// </summary>
    public Guid EquipmentId { get; set; }
    
    /// <summary>
    /// Навигационное свойство <see cref="Equipment"/>
    /// </summary>
    public Equipment? Equipment { get; set; }
}