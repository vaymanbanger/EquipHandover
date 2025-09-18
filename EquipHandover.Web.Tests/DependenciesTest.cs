using System.Reflection;
using EquipHandover.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EquipHandover.Web.Tests;

/// <summary>
/// Интеграционный тест для валидации конфигурации DI-контейнера
/// </summary>
public class DependenciesTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DependenciesTest"/>
    /// </summary>
    public DependenciesTest(WebApplicationFactory<Program> factory)
    {
        this.factory = factory.WithWebHostBuilder(builder => builder.ConfigureTestAppConfiguration());
    }

    /// <summary>
    /// Тест на проверку связанностей зависимостей 
    /// </summary>
    [Theory]
    [MemberData(nameof(WebControllerCore))]
    public void WebControllerCoreShouldBeResolved(Type controller)
    {
        // Arrange
        using var scope = factory.Services.CreateScope();
        
        // Act
        var instance = scope.ServiceProvider.GetRequiredService(controller);
        
        // Assert
        instance.Should().NotBeNull();
    }

    /// <summary>
    /// Возвращает все контроллеры, наследуемые от <see cref="DocumentController"/>
    /// </summary>
    public static TheoryData<Type> WebControllerCore => GetControllers<DocumentController>();

    /// <summary>
    /// Возвращает все контроллеры, наследуемые от базового типа
    /// </summary>
    private static TheoryData<Type> GetControllers<TController>() =>
        new(Assembly.GetAssembly(typeof(TController))
            ?.DefinedTypes
            .Where(type => typeof(TController).IsAssignableFrom(type) && !type.IsAbstract));
}