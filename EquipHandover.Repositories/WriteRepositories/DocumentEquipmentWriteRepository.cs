using EquipHandover.Common.Contracts;
using EquipHandover.Context.Contracts;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Repositories.Extensions;

namespace EquipHandover.Repositories.WriteRepositories;

/// <inheritdoc cref="IDocumentEquipmentWriteRepository"/>
public class DocumentEquipmentWriteRepository : BaseWriteRepository<Entities.DocumentEquipment>,
    IDocumentEquipmentWriteRepository,
    IRepositoryAnchor
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentEquipmentWriteRepository"/>
    /// </summary>
    public DocumentEquipmentWriteRepository(IWriter writer,
        IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}