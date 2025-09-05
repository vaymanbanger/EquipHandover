using EquipHandover.Common.Contracts;
using EquipHandover.Context.Contracts;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Repositories.Extensions;

namespace EquipHandover.Repositories.WriteRepositories;

/// <inheritdoc cref="IReceiverWriteRepository"/>
public class ReceiverWriteRepository : BaseWriteRepository<Entities.Receiver>,
    IReceiverWriteRepository,
    IRepositoryAnchor
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverWriteRepository"/>
    /// </summary>
    public ReceiverWriteRepository(IWriter writer,
        IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}