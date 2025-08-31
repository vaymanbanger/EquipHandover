using System.Text;

namespace EquipHandover.Web.Tests.Client;

/// <summary>
/// Класс клиента для работы с API
/// </summary>
public partial class EquipHandoverApiClient
{
    /// <summary>
    /// Пустая заглушка для логики перед отправкой запроса
    /// </summary>
    private Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string? url, CancellationToken cancellationToken)
        => Task.CompletedTask;

    /// <summary>
    /// Пустая заглушка для логики перед отправкой запроса ( через StringBuilder )
    /// </summary>
    private Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder url, CancellationToken cancellationToken)
        => Task.CompletedTask;

    /// <summary>
    /// Пустая заглушка для логики после получения ответа
    /// </summary>
    private Task ProcessResponseAsync(HttpClient client, HttpResponseMessage request, CancellationToken cancellationToken)
        => Task.CompletedTask;
}