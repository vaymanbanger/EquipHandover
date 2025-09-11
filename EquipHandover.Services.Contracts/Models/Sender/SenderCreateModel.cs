namespace EquipHandover.Services.Contracts.Models.Sender;

/// <summary>
/// Модель создания отправителя
/// </summary>
public class SenderCreateModel
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
    public string TaxPayerId { get; set; } = string.Empty;
}