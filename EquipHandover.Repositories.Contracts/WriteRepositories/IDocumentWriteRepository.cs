using EquipHandover.Context.Contracts;

namespace EquipHandover.Repositories.Contracts.WriteRepositories;

/// <summary>
/// Репозиторий записи сущности <see cref="Entities.Document"/>
/// </summary>
public interface IDocumentWriteRepository : IDbWriter<Entities.Document>
{
    
}