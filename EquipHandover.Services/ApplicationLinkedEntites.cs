using EquipHandover.Entities;

namespace EquipHandover.Services;

/// <summary>
/// Класс для выброса ошибки если не удалось найти id по модели
/// </summary>
public class ApplicationLinkedEntites
{
    /// <summary>
    /// Модель получающего
    /// </summary>
    public required Receiver Receiver { get; set; }
    
    /// <summary>
    /// Модель отправителя
    /// </summary>
    public required Sender Sender { get; set; }
    
    /// <summary>
    /// Модель оборудования
    /// </summary>
    public required IReadOnlyCollection<Equipment> Equipment { get; set; }
}