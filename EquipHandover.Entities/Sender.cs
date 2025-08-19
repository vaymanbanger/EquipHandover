namespace EquipHandover.Entities;
/// <summary>
/// Сущность отправителя
/// </summary>
public class Sender  : BaseAuditEntity
{
    /// <summary>
    /// Идентификатор отправителя
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
    /// ИНН
    /// </summary>
    public int Inn { get; set; }
}