using EquipHandover.Context.Contracts;

namespace EquipHandover.Entities;

/// <summary>
/// Модель сущности с аудитом
/// </summary>
public abstract class BaseAuditEntity : IEntitySoftDeleted,  IEntityWithId, IEntityWithAudit
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Дата изменения записи
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
    
    /// <summary>
    /// Дата удаления записи
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
    
}