using EquipHandover.Common.Contracts;
using EquipHandover.Context.Contracts;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Repositories.Extensions;

namespace EquipHandover.Repositories.WriteRepositories;

/// <inheritdoc cref="IDocumentWriteRepository"/>
public class EquipmentWriteRepository : BaseWriteRepository<Entities.Equipment>,
    IEquipmentWriteRepository,
    IRepositoryAnchor
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentWriteRepository"/>
    /// </summary>
    public EquipmentWriteRepository(IWriter writer,
        IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}