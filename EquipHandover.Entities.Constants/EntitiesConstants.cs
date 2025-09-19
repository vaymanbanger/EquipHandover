namespace EquipHandover.Entities.Constants;

/// <summary>
/// Константы для сущностей
/// </summary>
public static class EntitiesConstants
{
    /// <summary>
    /// Минимальная длина для всех свойств
    /// </summary>
    public const int MinLength = 3;
    
    /// <summary>
    /// Максимальная длина всех свойств
    /// </summary>
    public const int MaxLength = 255;
    
    /// <summary>
    /// Обязательная длина года выпуска
    /// </summary>
    public const int ManufactureYearLength = 4;
    
    /// <summary>
    /// Минимальный год выпуска
    /// </summary>
    public const int MinManufactureYear = 1900;
    
    /// <summary>
    /// Максимальный год выпуска
    /// </summary>
    public const int MaxManufactureYear = 2100;
    
    /// <summary>
    /// Максимальная длина номера подписания договора
    /// </summary>
    public const int MaxLengthSignatureNumber = 50;
    
    /// <summary>
    /// Максимальная длина города
    /// </summary>
    public const int MaxLengthCity = 100;
    
    /// <summary>
    /// Максимальная длина полного имени
    /// </summary>
    public const int MaxLengthFullName = 50;
    
    /// <summary>
    /// Максимальная длина предприятия
    /// </summary>
    public const int MaxLengthEnterprise = 90;
    
    /// <summary>
    /// Длина основного государственного регистрационного номера
    /// </summary>
    public const int RegistrationNumberLength = 13;
    
    /// <summary>
    /// Длина идентификационного номера налогоплательщика
    /// </summary>
    public const int TaxPayerNumLength = 10;
}