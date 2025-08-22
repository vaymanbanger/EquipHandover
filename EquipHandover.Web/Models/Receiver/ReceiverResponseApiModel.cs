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