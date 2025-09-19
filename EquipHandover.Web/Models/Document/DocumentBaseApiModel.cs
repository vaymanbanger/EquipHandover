namespace EquipHandover.Web.Models.Document;

/// <summary>
/// Базовая API модель для документа
/// </summary>
public class DocumentBaseApiModel
{
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
}