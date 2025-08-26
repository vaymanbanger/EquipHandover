using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Repositories.ReadRepositories;

/// <inheritdoc cref="IEquipmentReadRepository"/>
public class EquipmentReadRepository : IEquipmentReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentReadRepository"/>
    /// </summary>
    public EquipmentReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<Equipment?> IEquipmentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<Equipment>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Equipment>> IEquipmentReadRepository.GetAllAsync(CancellationToken cancellationToken)
        => reader.Read<Equipment>()
            .NotDeletedAt()
            .ToReadOnlyCollectionAsync(cancellationToken);
}