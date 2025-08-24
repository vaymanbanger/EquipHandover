using EquipHandover.Common;

namespace EquipHandover.Web.Infrastructure;

/// <inheritdoc cref="IDateTimeProvider"/>
public class DateTimeProvider : IDateTimeProvider
{
    DateTimeOffset IDateTimeProvider.UtcNow()
        => DateTimeOffset.UtcNow;

    DateTimeOffset IDateTimeProvider.Now()
        => DateTimeOffset.Now;
}