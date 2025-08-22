namespace EquipHandover.Web.Models.Receiver;

/// <summary>
/// API модель получателя
/// </summary>
public class ReceiverResponseApiModel
{
    /// <summary>
    /// Идентификатор принимающего
    /// </summary>
    public Guid Id { get; set; }
    
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
    public int RegistrationNumber { get; set; }
}