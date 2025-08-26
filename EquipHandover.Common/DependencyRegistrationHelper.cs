using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EquipHandover.Common;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/> для регистрации всех зависимостей приложения.
/// </summary>
public static class DependencyRegistrationHelper
{
    /// <summary>
    /// Регистрация с помощью маркерных интерфейсов
    /// </summary>
    public static void RegisterByInterface<TInterface>(this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var interfaceType = typeof(TInterface);
        var allTypes = interfaceType.Assembly.GetTypes()
            .Where(x => interfaceType.IsAssignableFrom(x)
                        && x.IsClass && !x.IsAbstract);

        foreach (var type in allTypes)
        {
            services.TryAdd(new ServiceDescriptor(type, type, lifetime));
            var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(x => x != typeof(IDisposable) && x.IsPublic && x != interfaceType);
            
            foreach (var serviceInterface in interfaces)
            {
                services.TryAdd(new ServiceDescriptor(serviceInterface, provider =>
                    provider.GetRequiredService(type), lifetime));
            }
        }
    }
}