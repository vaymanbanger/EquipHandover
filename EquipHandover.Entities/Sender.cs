namespace EquipHandover.Entities;
/// <summary>
/// Сущность отправителя
/// </summary>
public class Sender  : BaseAuditEntity
{
    /// <summary>
    /// Полное имя отправителя
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