namespace EquipHandover.Services.Contracts.Models.Sender;

/// <summary>
/// Модель отправителя
/// </summary>
public class SenderModel : SenderCreateModel
{
    /// <summary>
    /// Идентификатор отправителя
    /// </summary>
    public Guid Id { get; set; }
}