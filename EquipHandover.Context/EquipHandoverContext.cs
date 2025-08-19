using EquipHandover.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Context;
/// <summary>
/// Контекст БД
/// </summary>
public class EquipHandoverContext : DbContext
{
    /// <summary>
    /// ctor
    /// </summary>
    public EquipHandoverContext(DbContextOptions<EquipHandoverContext> options) : base(options)
    {
        // https://support.aspnetzero.com/QA/Questions/11011/Cannot-write-DateTime-with-KindLocal-to-PostgreSQL-type-%27timestamp-with-time-zone%27-only-UTC-is-supported
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    /// <summary>
    /// Настраивает модели БД
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfigurationAnchor).Assembly);
    }
}