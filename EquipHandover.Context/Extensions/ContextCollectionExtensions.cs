using EquipHandover.Context.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EquipHandover.Context.Extensions;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/> для регистрации всех зависимостей приложения.
/// </summary>
public static class ContextCollectionExtensions
{
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    public static void RegisterContextDependencies(this IServiceCollection service)
    {
        service.AddScoped<IReader>(x => x.GetRequiredService<EquipHandoverContext>());
        service.AddScoped<IWriter>(x => x.GetRequiredService<EquipHandoverContext>());
        service.AddScoped<IUnitOfWork>(x => x.GetRequiredService<EquipHandoverContext>());
    }
}