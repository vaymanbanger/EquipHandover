using System.Diagnostics.CodeAnalysis;
using EquipHandover.Common.Contracts;

namespace EquipHandover.Context.Contracts;

/// <summary>
/// Базовый класс репозитория записи данных
/// </summary>
public abstract class BaseWriteRepository<T> : IDbWriter<T> 
    where T : class
{
    private readonly IWriter writer;
    private readonly IDateTimeProvider dateTimeProvider;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="BaseWriteRepository{T}"/>
    /// </summary>
    protected BaseWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
    {
        this.writer = writer;
        this.dateTimeProvider = dateTimeProvider;
    }

    void IDbWriter<T>.Add([NotNull] T entity)
    {
        AuditCreate(entity);
        AuditUpdate(entity);
        writer.Add(entity);
    }

    void IDbWriter<T>.Update([NotNull] T entity)
    {
        AuditUpdate(entity);
        writer.Add(entity);
    }

    void IDbWriter<T>.Delete([NotNull] T entity)
    {
        AuditUpdate(entity);
        if (entity is IEntitySoftDeleted softDeletedEntity)
        {
            AuditUpdate(entity);
            softDeletedEntity.DeletedAt = dateTimeProvider.UtcNow();
            writer.Update(softDeletedEntity);
        }
        else
        {
            writer.Delete(entity);
        }
    }

    private void AuditCreate([NotNull] T entity)
    {
        if (entity is IEntityWithAudit auditCreated)
        {
            auditCreated.CreatedAt = dateTimeProvider.UtcNow();
        }
    }
    
    private void AuditUpdate([NotNull] T entity)
    {
        if (entity is IEntityWithAudit auditUpdated)
        {
            auditUpdated.UpdatedAt = dateTimeProvider.UtcNow();
        }
    }
}