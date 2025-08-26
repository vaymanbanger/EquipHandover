using AutoMapper;
using EquipHandover.Common.Contracts;
using EquipHandover.Services.AutoMappers;
using EquipHandover.Web.AutoMappers;
using EquipHandover.Web.Infrastructure;

namespace EquipHandover.Web.Extensions;

/// <summary>
/// Расширения <see cref="IServiceCollection"/> для регистрации всех зависимостей приложения.
/// </summary>
public static class WebServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует зависимости
    /// </summary>
    public static void RegisterWebDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IMapper>(_ =>
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApiMapper>();
                cfg.AddProfile<ServiceProfile>();
            });
            var mapper = mapperConfig.CreateMapper();
            return mapper;
        });
    }
}