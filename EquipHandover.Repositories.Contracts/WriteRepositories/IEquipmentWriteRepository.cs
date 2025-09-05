using EquipHandover.Context.Contracts;

namespace EquipHandover.Repositories.Contracts.WriteRepositories;

/// <summary>
/// Репозиторий записи сущности <see cref="Entities.Equipment"/>
/// </summary>
public interface IEquipmentWriteRepository : IDbWriter<Entities.Equipment>
{
    
}