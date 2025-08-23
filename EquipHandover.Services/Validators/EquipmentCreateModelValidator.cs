using EquipHandover.Services.Contracts.Models.Equipment;
using FluentValidation;

namespace EquipHandover.Services.Validators;

/// <summary>
/// Валидация <see cref="EquipmentCreateModel"/>
/// </summary>
public class EquipmentCreateModelValidator : AbstractValidator<EquipmentCreateModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;
    private const int ManufactureYearLength = 4;
    private const int MinManufactureYear = 1900;
    private const int MaxManufactureYear = 2100;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentCreateModelValidator"/>
    /// </summary>
    public EquipmentCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Наименование оборудования не может быть пустым")
            .Length(MinLength, MaxLength)
            .WithMessage($"Длина наименования оборудования должна быть от {MinLength} до {MaxLength}");

        RuleFor(x => x.ManufactureDate)
            .NotNull()
            .WithMessage("Год выпуска оборудования не может быть пустым")
            .Must(year => year.ToString().Length == ManufactureYearLength)
            .WithMessage($"Год выпуска оборудования должен состоять из {ManufactureYearLength} цифр")
            .InclusiveBetween(MinManufactureYear, MaxManufactureYear)
            .WithMessage($"Год выпуска оборудования должен быть между {MinManufactureYear} и {MaxManufactureYear}");

        RuleFor(x => x.SerialNumber)
            .NotEmpty()
            .WithMessage("Заводской номер оборудования не может быть пустым")
            .Length(MinLength, MaxLength)
            .WithMessage($"Длина заводской номер оборудования должен быть от {MinLength} до {MaxLength}");
        
        RuleFor(x => x.EquipmentNumber)
            .NotEmpty()
            .WithMessage("Номер оборудования не может быть пустым")
            .Length(MinLength, MaxLength)
            .WithMessage($"Длина номера оборудования должна быть от {MinLength} до {MaxLength}");
    }
}