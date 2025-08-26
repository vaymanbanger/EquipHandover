using EquipHandover.Context.Contracts;

namespace EquipHandover.Repositories.Contracts.WriteRepositories;

/// <summary>
/// Репозиторий записи сущности <see cref="Entities.Receiver"/>
/// </summary>
public interface IReceiverWriteRepository : IDbWriter<Entities.Receiver>
{
    
}