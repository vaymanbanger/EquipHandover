using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Repositories.ReadRepositories;

/// <inheritdoc cref="IReceiverReadRepository"/>
public class ReceiverReadRepository : IReceiverReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverReadRepository"/>
    /// </summary>
    public ReceiverReadRepository(IReader reader)
    {
        this.reader = reader;
    }
    
    Task<Receiver?> IReceiverReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<Receiver>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);
            

    Task<IReadOnlyCollection<Receiver>> IReceiverReadRepository.GetAllAsync(CancellationToken cancellationToken)
        => reader.Read<Receiver>()
            .NotDeletedAt()
            .ToReadOnlyCollectionAsync(cancellationToken);
}