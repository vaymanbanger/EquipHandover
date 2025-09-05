namespace EquipHandover.Services.Contracts.Models.Sender;

/// <summary>
/// Модель отправителя
/// </summary>
public class SenderModel
{
    /// <summary>
    /// Идентификатор отправителя
    /// </summary>
    public Guid Id { get; set; }
    
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
    public string TaxPayerId { get; set; } = string.Empty;
}