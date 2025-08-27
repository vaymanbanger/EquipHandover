using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Services.Validators.CreateModels;
using FluentValidation.TestHelper;
using Xunit;

namespace EquipHandover.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="EquipmentCreateModelValidator"/>
/// </summary>
public class EquipmentCreateModelValidatorTests
{
    private readonly EquipmentCreateModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentCreateModelValidatorTests"/>
    /// </summary>
    public EquipmentCreateModelValidatorTests()
    {
        validator = new EquipmentCreateModelValidator();
    }
    
    /// <summary>
    /// Тест на сообщение об ошибке пустых полей
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldHaveErrorMessage()
    {
        // Arrange
        var model = new EquipmentCreateModel()
        {
            Name = String.Empty,
            ManufactureDate = 0,
            EquipmentNumber = String.Empty,
            SerialNumber = String.Empty,
        };
        
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.ManufactureDate);
        result.ShouldHaveValidationErrorFor(x => x.EquipmentNumber);
        result.ShouldHaveValidationErrorFor(x => x.SerialNumber);
    }

    /// <summary>
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task FieldsShouldNotHaveErrorMessage()
    {
        // Arrange
        var model = new EquipmentCreateModel()
        {
            Name = "Насрудин",
            ManufactureDate = 2024,
            EquipmentNumber = "ZO342VVV",
            SerialNumber = "ZZ35122324VV",
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
        result.ShouldNotHaveValidationErrorFor(x => x.ManufactureDate);
        result.ShouldNotHaveValidationErrorFor(x => x.EquipmentNumber);
        result.ShouldNotHaveValidationErrorFor(x => x.SerialNumber);
    }
    
    /// <summary>
    /// Тест на минимальную длину оборудования
    /// </summary>
    [Fact]
    public async Task MinLengthShouldHaveErrorMessage()
    {
        // Arrange
        var model = new EquipmentCreateModel()
        {
            Name = "На",
            ManufactureDate = 202,
            EquipmentNumber = "ZO",
            SerialNumber = "ZZ",
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.ManufactureDate);
        result.ShouldHaveValidationErrorFor(x => x.EquipmentNumber);
        result.ShouldHaveValidationErrorFor(x => x.SerialNumber);
    }
    
    /// <summary>
    /// Тест на максимальную длину оборудования
    /// </summary>
    [Fact]
    public async Task MaxLengthShouldHaveErrorMessage()
    {
        // Arrange
        var model = new EquipmentCreateModel()
        {
            Name = new string('Z',299),
            ManufactureDate = 023456,
            EquipmentNumber = new string('O',299),
            SerialNumber = new string('V',299),
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.ManufactureDate);
        result.ShouldHaveValidationErrorFor(x => x.EquipmentNumber);
        result.ShouldHaveValidationErrorFor(x => x.SerialNumber);
    }
    
    /// <summary>
    /// Тест на минимальный год выпуска оборудования ( от 1900 до 2100г. )
    /// </summary>
    [Fact]
    public async Task MinManufactureYearShouldHaveErrorMessage()
    {
        // Arrange
        var model = new EquipmentCreateModel()
        {
            ManufactureDate = 1899,
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ManufactureDate);
    }
    
    /// <summary>
    /// Тест на максимальный год выпуска оборудования ( от 1900 до 2100г. )
    /// </summary>
    [Fact]
    public async Task MaxManufactureYearShouldHaveErrorMessage()
    {
        // Arrange
        var model = new EquipmentCreateModel()
        {
            ManufactureDate = 2101,
        };
            
        // Act
        var result = await validator.TestValidateAsync(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ManufactureDate);
    }
}