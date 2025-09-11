using EquipHandover.Common;
using EquipHandover.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EquipHandover.Services.Extensions;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/> для регистрации всех зависимостей приложения.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация сервисов как Scoped
    /// </summary>
    public static void RegisterServiceDependencies(this IServiceCollection services)
    {
        services.RegisterByInterface<IServiceAnchor>();
        services.AddSingleton<IValidateService, ValidateService>();
    }
}