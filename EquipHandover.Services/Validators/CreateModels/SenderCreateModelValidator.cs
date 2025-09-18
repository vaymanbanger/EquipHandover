using EquipHandover.Entities.Constants;
using EquipHandover.Services.Contracts.Models.Sender;
using FluentValidation;

namespace EquipHandover.Services.Validators.CreateModels;

/// <summary>
/// Валидация <see cref="SenderCreateModel"/>
/// </summary>
public class SenderCreateModelValidator : AbstractValidator<SenderCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderCreateModelValidator"/>
    /// </summary>
    public SenderCreateModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Полное имя принимающего не может быть пустым")
            .Length(EntitiesConstants.MinLength, EntitiesConstants.MaxLength)
            .WithMessage($"Длина полного имя принимающего должна быть от {EntitiesConstants.MinLength} до {EntitiesConstants.MaxLength}");
        
        RuleFor(x => x.Enterprise)
            .NotEmpty()
            .WithMessage("Предприятие принимающего не может быть пустым")
            .Length(EntitiesConstants.MinLength, EntitiesConstants.MaxLength)
            .WithMessage($"Длина предприятия принимающего должна быть от {EntitiesConstants.MinLength} до {EntitiesConstants.MaxLength}");
        
        RuleFor(x => x.TaxPayerNum)
            .NotEmpty()
            .WithMessage("Идентификационный номер налогоплательщика не может быть пустым")
            .Must(x => x.ToString().Length == EntitiesConstants.TaxPayerNumLength)
            .WithMessage($"Идентификационный номер налогоплательщика должен иметь {EntitiesConstants.TaxPayerNumLength} чисел");
    }
}