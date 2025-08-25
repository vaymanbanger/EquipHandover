using EquipHandover.Services.Contracts.Models.Receiver;
using FluentValidation;

namespace EquipHandover.Services.Validators;

/// <summary>
/// Валидация <see cref="ReceiverCreateModel"/>
/// </summary>
public class ReceiverCreateModelValidator : AbstractValidator<ReceiverCreateModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;
    private const int RegistrationNumberLength = 13;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverCreateModelValidator"/>
    /// </summary>
    public ReceiverCreateModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Полное имя принимающего не может быть пустым")
            .Length(MinLength, MaxLength)
            .WithMessage($"Длина полного имя принимающего должна быть от {MinLength} до {MaxLength}");
        
        RuleFor(x => x.Enterprise)
            .NotEmpty()
            .WithMessage("Предприятие принимающего не может быть пустым")
            .Length(MinLength, MaxLength)
            .WithMessage($"Длина предприятия принимающего должна быть от {MinLength} до {MaxLength}");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .WithMessage("Основной государственный регистрационный номер не может быть пустым")
            .Must(x => x.ToString().Length == RegistrationNumberLength)
            .WithMessage($"Основной государственный регистрационный номер должен иметь {RegistrationNumberLength} чисел");
    }
}