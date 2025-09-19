using EquipHandover.Common;
using Microsoft.Extensions.DependencyInjection;

namespace EquipHandover.Repositories.Extensions;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/> для регистрации всех зависимостей приложения.
/// </summary>
public static class RepositoryCollectionExtensions
{
    /// <summary>
    /// Регистрация сервисов как Scoped
    /// </summary>
    public static void RegisterRepositoryDependencies(this IServiceCollection services)
    {
        services.RegisterByInterface<IRepositoryAnchor>();
    }
}