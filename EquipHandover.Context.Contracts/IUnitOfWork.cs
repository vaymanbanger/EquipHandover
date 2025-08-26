namespace EquipHandover.Context.Contracts;

/// <summary>
/// Определяет интерфейс для Unit Of Work
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Асинхронно сохраняет все изменения контекста
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}