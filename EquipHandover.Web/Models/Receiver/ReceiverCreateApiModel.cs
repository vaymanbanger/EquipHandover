namespace EquipHandover.Web.Models.Receiver;

/// <summary>
/// API модель для создания/редактирования получателя
/// </summary>
public class ReceiverCreateApiModel
{
    /// <summary>
    /// Полное имя принимающего
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Предприятие
    /// </summary>
    public string Enterprise { get; set; } = string.Empty;
    
    /// <summary>
    /// Основной государственный регистрационный номер
    /// </summary>
    public string RegistrationNumber { get; set; } = string.Empty;
}