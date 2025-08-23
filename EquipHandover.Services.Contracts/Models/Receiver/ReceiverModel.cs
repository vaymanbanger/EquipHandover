namespace EquipHandover.Services.Contracts.Models.Receiver;

/// <summary>
/// Модель принимающего
/// </summary>
public class ReceiverModel
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
    public string RegistrationNumber { get; set; } = string.Empty;
}