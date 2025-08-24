namespace EquipHandover.Services.Contracts.Exceptions;

/// <summary>
/// Ошибки операции недопустимой для текущего состояния объекта
/// </summary>
public class EquipHandoverInvalidOperationException : EquipHandoverException
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverInvalidOperationException"/> с указанием
    /// сообщения об ошибке
    /// </summary>
    public EquipHandoverInvalidOperationException(string message)
    : base(message)
    {
        
    }
}