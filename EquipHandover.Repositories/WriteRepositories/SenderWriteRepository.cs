using EquipHandover.Common.Contracts;
using EquipHandover.Context.Contracts;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Repositories.Extensions;

namespace EquipHandover.Repositories.WriteRepositories;

/// <inheritdoc cref="ISenderWriteRepository"/>
public class SenderWriteRepository : BaseWriteRepository<Entities.Sender>,
    ISenderWriteRepository,
    IRepositoryAnchor
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverWriteRepository"/>
    /// </summary>
    public SenderWriteRepository(IWriter writer,
        IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}