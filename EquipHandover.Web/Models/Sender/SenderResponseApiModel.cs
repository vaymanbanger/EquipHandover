namespace EquipHandover.Web.Models.Sender;

/// <summary>
/// API модель отправителя
/// </summary>
public class SenderResponseApiModel
{
    /// <summary>
    /// Идентификатор отправителя
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Предприятие
    /// </summary>
    public string Enterprise { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификационный номер налогоплательщика
    /// </summary>
    public int TaxPayerId { get; set; }
}