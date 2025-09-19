namespace EquipHandover.Web.Models.Sender;

/// <summary>
/// API модель для создания/редактирования отправителя
/// </summary>
public class SenderCreateApiModel
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
    public string TaxPayerNum { get; set; }  = string.Empty;
}