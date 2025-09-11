namespace EquipHandover.Context.Contracts;

/// <summary>
/// Интерфейс получение записей из контекста
/// </summary>
public interface IReader
{
    /// <summary>
    /// Предоставляет функиональные возможности для выполнения запросов
    /// </summary>
    IQueryable<TEntity> Read<TEntity>() where TEntity : class;
}