using EquipHandover.Services.Contracts.Models.Receiver;
using EquipHandover.Services.Validators.ResponseModels;
using FluentValidation.TestHelper;
using Xunit;

namespace EquipHandover.Services.Tests.Validators.ResponseModels;

/// <summary>
/// Тесты для <see cref="ReceiverModelValidator"/>
/// </summary>
public class ReceiverModelValidatorTests
{
    private readonly ReceiverModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverModelValidatorTests"/>
    /// </summary>
    public ReceiverModelValidatorTests()
    {
        validator = new ReceiverModelValidator();
    }
    
    /// <summary>
    /// Тест на сообщение об ошибке пустых полей
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldHaveErrorMessage()
    {
        // Arrange
        var model = new ReceiverModel()
        {
            FullName = string.Empty,
            RegistrationNumber = string.Empty,
            Enterprise = string.Empty,
        };
        
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.RegistrationNumber);
        result.ShouldHaveValidationErrorFor(x => x.Enterprise);
    }
    
    /// <summary>
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldNotHaveErrorMessage()
    {
        // Arrange
        var model = new ReceiverModel()
        {
            FullName = "Карасев Маслина Поймалович",
            RegistrationNumber = "1234567890123",
            Enterprise = "ООО Пердович",
        };
        
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.FullName);
        result.ShouldNotHaveValidationErrorFor(x => x.RegistrationNumber);
        result.ShouldNotHaveValidationErrorFor(x => x.Enterprise);
    }
    
    /// <summary>
    /// Тест на минимальную длину оборудования
    /// </summary>
    [Fact]
    public async Task MinLengthShouldHaveErrorMessage()
    {
        // Arrange
        var model = new ReceiverModel()
        {
            FullName = "Ка",
            RegistrationNumber = "1234567890",
            Enterprise = "О",
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.RegistrationNumber);
        result.ShouldHaveValidationErrorFor(x => x.Enterprise);
    }
    
    /// <summary>
    /// Тест на максимальную длину оборудования
    /// </summary>
    [Fact]
    public async Task MaxLengthShouldHaveErrorMessage()
    {
        // Arrange
        var model = new ReceiverModel()
        {
            FullName = new string('Z',299),
            RegistrationNumber = new string('O',14),
            Enterprise = new string('V',299),
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.RegistrationNumber);
        result.ShouldHaveValidationErrorFor(x => x.Enterprise);
    }
}