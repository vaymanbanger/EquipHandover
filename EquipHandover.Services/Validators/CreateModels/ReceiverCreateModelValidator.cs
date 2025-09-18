using EquipHandover.Entities.Constants;
using EquipHandover.Services.Contracts.Models.Receiver;
using FluentValidation;

namespace EquipHandover.Services.Validators.CreateModels;

/// <summary>
/// Валидация <see cref="ReceiverCreateModel"/>
/// </summary>
public class ReceiverCreateModelValidator : AbstractValidator<ReceiverCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverCreateModelValidator"/>
    /// </summary>
    public ReceiverCreateModelValidator()
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

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .WithMessage("Основной государственный регистрационный номер не может быть пустым")
            .Must(x => x.ToString().Length == EntitiesConstants.RegistrationNumberLength)
            .WithMessage($"Основной государственный регистрационный номер должен иметь {EntitiesConstants.RegistrationNumberLength} чисел");
    }
}