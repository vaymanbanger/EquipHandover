namespace EquipHandover.Services.Contracts.Models.Sender;

/// <summary>
/// Модель создания отправителя
/// </summary>
public class SenderCreateModel
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