namespace EquipHandover.Web.Models.Sender;

/// <summary>
/// API модель для создания отправителя
/// </summary>
public class SenderRequestApiModel
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Предприятие
    /// </summary>
    public string Enterprise { get; set; } = string.Empty;
    
    /// <summary>
    /// ИНН
    /// </summary>
    public int Inn { get; set; }
}