namespace EquipHandover.Context.Contracts;

/// <summary>
/// Сущность с полями аудита 
/// </summary>
public interface IEntityWithAudit
{
    /// <summary>
    /// Дата добавления записи
    /// </summary>
    DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Дата обновления записи
    /// </summary>
    DateTimeOffset UpdatedAt { get; set; }
}