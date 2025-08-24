using System.Diagnostics.CodeAnalysis;

namespace EquipHandover.Context.Contracts;

/// <summary>
/// Интерфейс создания и модификации записей в хранилище
/// </summary>
public interface IDbWriter<in TEntity> where TEntity : class
{
    /// <summary>
    /// Добавить новую запись
    /// </summary>
    void Add([NotNull] TEntity entity);
    
    /// <summary>
    /// Редактировать запись
    /// </summary>
    void Update([NotNull] TEntity entity);
    
    /// <summary>
    /// Удалить запись
    /// </summary>
    void Delete([NotNull] TEntity entity);
}