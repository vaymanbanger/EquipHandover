using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Repositories.ReadRepositories;

/// <inheritdoc cref="ISenderReadRepository"/>
public class SenderReadRepository : ISenderReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderReadRepository  "/>
    /// </summary>
    /// <param name="reader"></param>
    public SenderReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<Sender?> ISenderReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<Sender>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Sender>> ISenderReadRepository.GetAllAsync(CancellationToken cancellationToken)
        => reader.Read<Sender>()
            .NotDeletedAt()
            .ToReadOnlyCollectionAsync(cancellationToken);
}