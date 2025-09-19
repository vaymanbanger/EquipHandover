namespace EquipHandover.Services.Contracts.Models.Receiver;

/// <summary>
/// Модель принимающего
/// </summary>
public class ReceiverModel : ReceiverCreateModel
{
    /// <summary>
    /// Идентификатор принимающего
    /// </summary>
    public Guid Id { get; set; }
}