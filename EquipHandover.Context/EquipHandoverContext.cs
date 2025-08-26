using System.Diagnostics.CodeAnalysis;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Context;
/// <summary>
/// Контекст базы данных
/// </summary>
public class EquipHandoverContext : DbContext, IReader, IWriter, IUnitOfWork
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipHandoverContext"/>
    /// </summary>
    public EquipHandoverContext(DbContextOptions<EquipHandoverContext> options) : base(options)
    {
        // https://support.aspnetzero.com/QA/Questions/11011/Cannot-write-DateTime-with-KindLocal-to-PostgreSQL-type-%27timestamp-with-time-zone%27-only-UTC-is-supported
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    /// <summary>
    /// Настраивает модели базы данных
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfigurationAnchor).Assembly);
    }

    IQueryable<TEntity> IReader.Read<TEntity>()
        where TEntity : class
        => base.Set<TEntity>()
            .AsNoTracking()
            .AsQueryable();

    void IWriter.Add<TEntity>([NotNull] TEntity entity) where TEntity : class
        => base.Entry(entity).State = EntityState.Added;
    void IWriter.Update<TEntity>([NotNull] TEntity entity) where TEntity : class
        => base.Entry(entity).State = EntityState.Modified;
    void IWriter.Delete<TEntity>([NotNull] TEntity entity) where TEntity : class
        => base.Entry(entity).State = EntityState.Deleted;

    async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
    {
        var count = await base.SaveChangesAsync(cancellationToken);
        foreach (var entry in base.ChangeTracker.Entries().ToArray())
        {
            entry.State = EntityState.Detached;
        }
        
        return count;
    }
}