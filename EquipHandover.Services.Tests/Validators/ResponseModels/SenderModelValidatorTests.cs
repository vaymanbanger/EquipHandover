using EquipHandover.Services.Contracts.Models.Sender;
using EquipHandover.Services.Validators.ResponseModels;
using FluentValidation.TestHelper;
using Xunit;

namespace EquipHandover.Services.Tests.Validators.ResponseModels;

/// <summary>
/// Тесты для <see cref="SenderModelValidator"/>
/// </summary>
public class SenderModelValidatorTests
{
    private readonly SenderModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderModelValidatorTests"/>
    /// </summary>
    public SenderModelValidatorTests()
    {
        validator = new SenderModelValidator();
    }
    
     /// <summary>
    /// Тест на сообщение об ошибке пустых полей
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldHaveErrorMessage()
    {
        // Arrange
        var model = new SenderModel()
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
        var model = new SenderModel()
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
        var model = new SenderModel()
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
        var model = new SenderModel()
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