namespace EquipHandover.Web.Models.Equipment;

/// <summary>
/// API модель оборудования
/// </summary>
public class EquipmentResponseApiModel : EquipmentCreateApiModel
{
    /// <summary>
    /// Идентификатор оборудования
    /// </summary>
    public Guid Id{ get; set; }
}