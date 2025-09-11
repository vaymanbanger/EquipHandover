namespace EquipHandover.Services.Contracts.Models.Receiver;

/// <summary>
/// Модель создания принимающего
/// </summary>
public class ReceiverCreateModel
{
    /// <summary>
    /// Полное имя принимаюшего
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