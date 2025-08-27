using EquipHandover.Services.Contracts.Models.Document;
using FluentValidation;

namespace EquipHandover.Services.Validators.ResponseModels;

/// <summary>
/// Валидация <see cref="DocumentModel"/>
/// </summary>
public class DocumentModelValidator : AbstractValidator<DocumentModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentModelValidator"/>
    /// </summary>
    public DocumentModelValidator()
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

        RuleFor(x => x.Receiver)
            .NotEmpty()
            .WithMessage("Поле с принимающем не может быть пустым");

        RuleFor(x => x.Equipment)
            .NotEmpty()
            .WithMessage("Поле с оборудованием не может быть пустым");

        RuleFor(x => x.Sender)
            .NotEmpty()
            .WithMessage("Поле с отправителем не может быть пустым");
    }
}