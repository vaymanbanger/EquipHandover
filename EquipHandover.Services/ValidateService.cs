using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Services.Contracts.Models.Receiver;
using EquipHandover.Services.Contracts.Models.Sender;
using EquipHandover.Services.Validators.CreateModels;
using EquipHandover.Services.Validators.ResponseModels;
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
        validators.TryAdd(typeof(SenderCreateModel), new SenderCreateModelValidator());
        validators.TryAdd(typeof(ReceiverCreateModel), new ReceiverCreateModelValidator());
        validators.TryAdd(typeof(EquipmentCreateModel), new EquipmentCreateModelValidator());
        
        validators.TryAdd(typeof(DocumentModel), new DocumentModelValidator());
        validators.TryAdd(typeof(SenderModel), new SenderModelValidator());
        validators.TryAdd(typeof(ReceiverModel), new ReceiverModelValidator());
        validators.TryAdd(typeof(EquipmentModel), new EquipmentModelValidator());
    }
    
    async Task IValidateService.ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
        where TModel : class
    {
        if (!validators.TryGetValue(model.GetType(), out var validator))
        {
            throw new InvalidOperationException($"Запрашиваемый валидатор {model.GetType()} не найден. ");
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