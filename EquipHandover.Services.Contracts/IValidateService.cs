namespace EquipHandover.Services.Contracts;

/// <summary>
/// Интерфейс валидации сервиса
/// </summary>
public interface IValidateService
{
    /// <summary>
    /// Валидация модели
    /// </summary>
    Task ValidateAsync<TModel>(TModel model,CancellationToken cancellationToken)
    where TModel : class;
}