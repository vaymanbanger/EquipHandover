using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Validators.ResponseModels;
using FluentValidation.TestHelper;
using Xunit;

namespace EquipHandover.Services.Tests.Validators.ResponseModels;

/// <summary>
/// Тесты для <see cref="DocumentModelValidator"/>
/// </summary>
public class DocumentModelValidatorTests
{
    private readonly DocumentModelValidator validator;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentModelValidatorTests"/>
    /// </summary>
    public DocumentModelValidatorTests()
    {
        validator = new DocumentModelValidator();
    }
    
     /// <summary>
    /// Тест на сообщение об ошибке пустых полей
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldHaveErrorMessage()
    {
        // Arrange
        var model = new DocumentModel()
        {
            City = "",
            RentalDate = new DateOnly(),
            SignatureNumber = new DateOnly()
        };
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City);
        result.ShouldHaveValidationErrorFor(x => x.SignatureNumber);
        result.ShouldHaveValidationErrorFor(x => x.RentalDate);
    }
    
    /// <summary>
    /// Тест на сообщение об ошибке c прошлым временем
    /// </summary>
    [Fact]
    public async Task InPastShouldHaveErrorMessage()
    {
        // Arrange
        var model = new DocumentModel()
        {
            City = "Нью-Йорк",
            RentalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)),
            SignatureNumber = DateOnly.FromDateTime(DateTime.Now.AddDays(-2))
        };
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SignatureNumber);
        result.ShouldHaveValidationErrorFor(x => x.RentalDate);
    }
    
    /// <summary>
    /// Тест на отсутствие ошибок c будущим и настоящим временем
    /// </summary>
    [Fact]
    public async Task ForFutureOrNowShouldNotHaveErrorMessage()
    {
        // Arrange
        var model = new DocumentModel()
        {
            City = "Нью-Йорк",
            RentalDate = DateOnly.FromDateTime(DateTime.Now),
            SignatureNumber = DateOnly.FromDateTime(DateTime.Now.AddDays(7))
        };
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.SignatureNumber);
        result.ShouldNotHaveValidationErrorFor(x => x.RentalDate);
    }
}