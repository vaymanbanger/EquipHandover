namespace EquipHandover.Web.Models.Sender;

/// <summary>
/// API модель для создания отправителя
/// </summary>
public class SenderRequestApiModel
{
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
    public string TaxPayerId { get; set; }  = string.Empty;
}