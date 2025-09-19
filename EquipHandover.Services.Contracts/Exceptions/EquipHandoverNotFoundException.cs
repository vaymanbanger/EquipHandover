namespace EquipHandover.Services.Contracts.Exceptions;

/// <summary>
/// Ошибки при попытке доступа к несуществующему объекту
/// </summary>
public class EquipHandoverNotFoundException : EquipHandoverException
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverNotFoundException"/>
    /// </summary>
    public EquipHandoverNotFoundException(string message)
        : base(message)
    {
        
    }
}