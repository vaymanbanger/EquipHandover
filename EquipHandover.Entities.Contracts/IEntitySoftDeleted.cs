namespace EquipHandover.Entities.Contracts;

/// <summary>
/// Сущность с мягким удалением
/// </summary>
public interface IEntitySoftDeleted
{
    /// <summary>
    /// Дата удаления записи
    /// </summary>
    DateTimeOffset? DeletedAt { get; set; }
}