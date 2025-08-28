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
    
    Task<Document?> IDocumentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<DocumentDbModel>> IDocumentReadRepository.GetAllAsync(CancellationToken cancellationToken)
        => reader.Read<Document>()
            .NotDeletedAt()
            .Select(x => new DocumentDbModel()
            {
                SignatureNumber = x.SignatureNumber,
                RentalDate = x.RentalDate,
                Receiver = x.Receiver!,
                Sender = x.Sender!,
                Equipment = x.DocumentEquipments
                    .Where(y => y.DeletedAt == null)
                    .Select(y => y.Equipment!),
                City = x.City,
                Id = x.Id
            })
            .ToReadOnlyCollectionAsync(cancellationToken);
}