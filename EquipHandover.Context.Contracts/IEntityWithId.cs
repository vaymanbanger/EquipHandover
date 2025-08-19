namespace EquipHandover.Context.Contracts;

/// <summary>
/// Сущность с идентификатором
/// </summary>
public interface IEntityWithId
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    Guid Id { get; set; }
}