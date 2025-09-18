using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.Models;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Repositories.ReadRepositories;

/// <inheritdoc cref="IDocumentReadRepository"/>
public class DocumentReadRepository : IDocumentReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentReadRepository"/>
    /// </summary>
    public DocumentReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<IReadOnlyCollection<DocumentDbModel>> IDocumentReadRepository.GetAllAsync(CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .SelectFullModel()
            .ToReadOnlyCollectionAsync(cancellationToken);

    Task<DocumentDbModel?> IDocumentReadRepository.GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .ById(id)
            .SelectFullModel()
            .FirstOrDefaultAsync(cancellationToken);
}