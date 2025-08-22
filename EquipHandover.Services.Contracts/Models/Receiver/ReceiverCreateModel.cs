namespace EquipHandover.Services.Contracts.Models.Receiver;

/// <summary>
/// Модель создания принимающего
/// </summary>
public class ReceiverCreateModel
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
    /// ОГРН
    /// </summary>
    public int Ogrn { get; set; }
}