namespace EquipHandover.Services.Contracts;

/// <summary>
/// Интерфейс валидации сервиса
/// </summary>
public interface IValidateService
{
    Task ValidateAsync<TModel>(TModel model,CancellationToken cancellationToken)
    where TModel : class;
}