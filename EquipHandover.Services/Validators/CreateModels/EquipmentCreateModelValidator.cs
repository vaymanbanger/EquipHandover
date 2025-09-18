using EquipHandover.Entities.Constants;
using EquipHandover.Services.Contracts.Models.Equipment;
using FluentValidation;
namespace EquipHandover.Services.Validators.CreateModels;

/// <summary>
/// Валидация <see cref="EquipmentCreateModel"/>
/// </summary>
public class EquipmentCreateModelValidator : AbstractValidator<EquipmentCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentCreateModelValidator"/>
    /// </summary>
    public EquipmentCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Наименование оборудования не может быть пустым")
            .Length(EntitiesConstants.MinLength, EntitiesConstants.MaxLength)
            .WithMessage($"Длина наименования оборудования должна быть от {EntitiesConstants.MinLength} до {EntitiesConstants.MaxLength}");

        RuleFor(x => x.ManufacturedYear)
            .NotNull()
            .WithMessage("Год выпуска оборудования не может быть пустым")
            .Must(year => year.ToString().Length == EntitiesConstants.ManufactureYearLength)
            .WithMessage($"Год выпуска оборудования должен состоять из {EntitiesConstants.ManufactureYearLength} цифр")
            .InclusiveBetween(EntitiesConstants.MinManufactureYear, EntitiesConstants.MaxManufactureYear)
            .WithMessage($"Год выпуска оборудования должен быть между {EntitiesConstants.MinManufactureYear} и {EntitiesConstants.MaxManufactureYear}");

        RuleFor(x => x.SerialNumber)
            .NotEmpty()
            .WithMessage("Заводской номер оборудования не может быть пустым")
            .Length(EntitiesConstants.MinLength, EntitiesConstants.MaxLength)
            .WithMessage($"Длина заводской номер оборудования должен быть от {EntitiesConstants.MinLength} до {EntitiesConstants.MaxLength}");
    }
}