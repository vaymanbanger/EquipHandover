using EquipHandover.Services.Contracts.Models.Sender;
using EquipHandover.Services.Validators.CreateModels;
using FluentValidation.TestHelper;
using Xunit;

namespace EquipHandover.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="SenderCreateModelValidator"/>
/// </summary>
public class SenderCreateModelValidatorTests
{
     private readonly SenderCreateModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderCreateModelValidatorTests"/>
    /// </summary>
    public SenderCreateModelValidatorTests()
    {
        validator = new SenderCreateModelValidator();
    }
    
    /// <summary>
    /// Тест на сообщение об ошибке пустых полей
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldHaveErrorMessage()
    {
        // Arrange
        var model = new SenderCreateModel()
        {
            FullName = string.Empty,
            TaxPayerId = string.Empty,
            Enterprise = string.Empty,
        };
        
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.TaxPayerId);
        result.ShouldHaveValidationErrorFor(x => x.Enterprise);
    }
    
    /// <summary>
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldNotHaveErrorMessage()
    {
        // Arrange
        var model = new SenderCreateModel()
        {
            FullName = "Карасев Маслина Поймалович",
            TaxPayerId = "1234567890",
            Enterprise = "ООО Пердович",
        };
        
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.FullName);
        result.ShouldNotHaveValidationErrorFor(x => x.TaxPayerId);
        result.ShouldNotHaveValidationErrorFor(x => x.Enterprise);
    }
    
    /// <summary>
    /// Тест на минимальную длину оборудования
    /// </summary>
    [Fact]
    public async Task MinLengthShouldHaveErrorMessage()
    {
        // Arrange
        var model = new SenderCreateModel()
        {
            FullName = "Ка",
            TaxPayerId = "123456789",
            Enterprise = "О",
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.TaxPayerId);
        result.ShouldHaveValidationErrorFor(x => x.Enterprise);
    }
    
    /// <summary>
    /// Тест на максимальную длину оборудования
    /// </summary>
    [Fact]
    public async Task MaxLengthShouldHaveErrorMessage()
    {
        // Arrange
        var model = new SenderCreateModel()
        {
            FullName = new string('Z',299),
            TaxPayerId = new string('O',11),
            Enterprise = new string('V',299),
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.TaxPayerId);
        result.ShouldHaveValidationErrorFor(x => x.Enterprise);
    }
}