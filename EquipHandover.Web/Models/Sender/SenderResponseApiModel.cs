namespace EquipHandover.Web.Models.Sender;

/// <summary>
/// API модель отправителя
/// </summary>
public class SenderResponseApiModel : SenderCreateApiModel
{
    /// <summary>
    /// Идентификатор отправителя
    /// </summary>
    public Guid Id { get; set; }
}