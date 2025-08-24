using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Validators;
using FluentValidation;


namespace EquipHandover.Services;

/// <inheritdoc cref="IValidateService"/>
public class ValidateService : IValidateService
{
    private readonly IDictionary<Type, IValidator> validators;

    /// <summary>
    /// Инициализирует новый экземлпяр <see cref="ValidateService"/>
    /// </summary>
    public ValidateService()
    {
        validators = new Dictionary<Type, IValidator>();
        validators.TryAdd(typeof(DocumentCreateModel), new DocumentCreateModelValidator());
    }
    
    async Task IValidateService.ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
        where TModel : class
    {
        if (!validators.TryGetValue(model.GetType(), out var validator))
        {
            throw new EquipHandoverInvalidOperationException($"Не найден запрашиваемый валидатор: {model.GetType()}");
        }
        
        var context = new ValidationContext<TModel>(model);
        var validationResult = await validator.ValidateAsync(context, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new EquipHandoverValidationException(validationResult.Errors
                .Select(x => InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
        }
    }
}