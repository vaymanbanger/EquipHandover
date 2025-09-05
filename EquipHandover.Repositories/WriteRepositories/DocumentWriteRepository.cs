using EquipHandover.Common.Contracts;
using EquipHandover.Context.Contracts;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Repositories.Extensions;

namespace EquipHandover.Repositories.WriteRepositories;

/// <inheritdoc cref="IDocumentWriteRepository"/>
public class DocumentWriteRepository : BaseWriteRepository<Entities.Document>,
    IDocumentWriteRepository,
    IRepositoryAnchor
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentWriteRepository"/>
    /// </summary>
    public DocumentWriteRepository(IWriter writer,
        IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}