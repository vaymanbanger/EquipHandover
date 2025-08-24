using AutoMapper;
using EquipHandover.Services.AutoMappers;
using Xunit;

namespace EquipHandover.Services.Tests;

/// <summary>
/// Тесты профилей автомаппера
/// </summary>
public class AutoMapperProfileTests
{
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AutoMapperProfileTests"/>
    /// </summary>
    public AutoMapperProfileTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });
        
        mapper = config.CreateMapper();
    }

    /// <summary>
    /// Маппер правильно сформирован
    /// </summary>
    [Fact]
    public void ValidateMapperConfiguration()
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}