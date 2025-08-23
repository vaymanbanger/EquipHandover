using EquipHandover.Services.Contracts.Models.Sender;
using FluentValidation;

namespace EquipHandover.Services.Validators;

/// <summary>
/// Валидация <see cref="SenderCreateModel"/>
/// </summary>
public class SenderCreateModelValidator : AbstractValidator<SenderCreateModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;
    private const int TaxPayerIdLength = 13;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderCreateModelValidator"/>
    /// </summary>
    public SenderCreateModelValidator()
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
        
        RuleFor(x => x.TaxPayerId)
            .NotEmpty()
            .WithMessage("Идентификационный номер налогоплательщика не может быть пустым")
            .Must(x => x.ToString().Length == TaxPayerIdLength)
            .WithMessage($"Идентификационный номер налогоплательщика должен иметь {TaxPayerIdLength} чисел");
    }
}