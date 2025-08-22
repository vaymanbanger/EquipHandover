namespace EquipHandover.Web.Models.Receiver;

/// <summary>
/// API модель для создания получателя
/// </summary>
public class ReceiverRequestApiModel
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