using EquipHandover.Context.Contracts;

namespace EquipHandover.Repositories.Contracts.WriteRepositories;

/// <summary>
/// Репозиторий записи сущности <see cref="Entities.DocumentEquipment"/>
/// </summary>
public interface IDocumentEquipmentWriteRepository : IDbWriter<Entities.DocumentEquipment>
{
    
}