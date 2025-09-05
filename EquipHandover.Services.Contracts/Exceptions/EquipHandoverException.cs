namespace EquipHandover.Services.Contracts.Exceptions;

/// <summary>
/// Базовый класс исключений товаров
/// </summary>
public abstract class EquipHandoverException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverException"/>
    /// </summary>
    protected EquipHandoverException() { }
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverException"/> с указанием
    /// сообщения об ошибке
    /// </summary>
    protected EquipHandoverException(string message) 
        : base(message) { }
}