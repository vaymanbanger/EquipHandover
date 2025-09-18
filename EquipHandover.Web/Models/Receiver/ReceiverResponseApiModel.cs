namespace EquipHandover.Web.Models.Receiver;

/// <summary>
/// API модель получателя
/// </summary>
public class ReceiverResponseApiModel : ReceiverCreateApiModel
{
    /// <summary>
    /// Идентификатор принимающего
    /// </summary>
    public Guid Id { get; set; }
}