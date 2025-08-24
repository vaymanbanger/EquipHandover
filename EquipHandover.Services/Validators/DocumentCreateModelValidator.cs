using EquipHandover.Services.Contracts.Models.Document;
using FluentValidation;

namespace EquipHandover.Services.Validators;

/// <summary>
/// Валидация <see cref="DocumentCreateModel"/>
/// </summary>
public class DocumentCreateModelValidator : AbstractValidator<DocumentCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentCreateModelValidator"/>
    /// </summary>
    public DocumentCreateModelValidator()
    {
        RuleFor(x => x.RentalDate)
            .NotEmpty().WithMessage("Дата аренды оборудования не может быть пустым")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Дата аренды оборудования не может быть в прошлом");
        
        RuleFor(x => x.SignatureNumber)
            .NotEmpty().WithMessage("Номер подписания договора не может быть пустым")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Номер подписания договора не может быть в прошлом");
    
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Город не может быть пустым");

    }
}