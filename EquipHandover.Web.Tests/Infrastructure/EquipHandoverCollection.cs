using Xunit;

namespace EquipHandover.Web.Tests.Infrastructure;

/// <summary>
/// Коллекция для интеграционных тестов
/// </summary>
[CollectionDefinition(nameof(EquipHandoverCollection))]
public class EquipHandoverCollection : ICollectionFixture<EquipHandoverApiFixture>
{
    
}