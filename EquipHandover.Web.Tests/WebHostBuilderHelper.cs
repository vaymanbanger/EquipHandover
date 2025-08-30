using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EquipHandover.Web.Tests;

/// <summary>
/// Вспомогательные методы для настройки конфигурации тестового веб-хоста.
/// </summary>
internal static class WebHostBuilderHelper
{
    
    /// <summary>
    /// Настраивает конфигурацию приложения для интеграционных тестов.
    /// </summary>
    public static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
    {
        builder.UseEnvironment(TestsConstants.IntegrationEnvironment);
        builder.ConfigureAppConfiguration((_, config) =>
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.integration.json");
            config.AddJsonFile(configPath).AddEnvironmentVariables();
        });
    }
}