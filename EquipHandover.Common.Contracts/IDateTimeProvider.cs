namespace EquipHandover.Common.Contracts;

/// <summary>
/// Интерфейс для предоставления текущего времени
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Получает текущую дату и время в формате UTC
    /// </summary>
    DateTimeOffset UtcNow();

    /// <summary>
    /// Получает текущую дату и время в локальном часовом поясе
    /// </summary>
    DateTimeOffset Now();
}