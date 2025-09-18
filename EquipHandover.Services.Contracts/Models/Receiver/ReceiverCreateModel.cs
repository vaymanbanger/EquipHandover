namespace EquipHandover.Services.Contracts.Models.Receiver;

/// <summary>
/// Модель создания/редактирования принимающего
/// </summary>
public class ReceiverCreateModel
{
    /// <summary>
    /// Полное имя принимащшего
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