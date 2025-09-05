namespace EquipHandover.Services.Contracts.Exceptions;

/// <summary>
/// Ошибки валидации
/// </summary>
public class EquipHandoverValidationException : EquipHandoverException
{
    /// <summary>
    /// Ошибки
    /// </summary>
    public IEnumerable<InvalidateItemModel> Errors { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverValidationException"/>
    /// </summary>
    public EquipHandoverValidationException(IEnumerable<InvalidateItemModel> errors)
    {
        Errors = errors;
    }
}