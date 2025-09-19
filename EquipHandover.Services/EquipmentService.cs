using AutoMapper;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Services.Extensions;

namespace EquipHandover.Services;

/// <inheritdoc cref="IEquipmentService"/>
public class EquipmentService : IEquipmentService, IServiceAnchor
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IEquipmentReadRepository equipmentReadRepository;
    private readonly IEquipmentWriteRepository equipmentWriteRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentService"/>
    /// </summary>
    public EquipmentService(IMapper mapper,
        IUnitOfWork unitOfWork,
        IEquipmentReadRepository equipmentReadRepository,
        IEquipmentWriteRepository equipmentWriteRepository)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.equipmentReadRepository = equipmentReadRepository;
        this.equipmentWriteRepository = equipmentWriteRepository;
    }

    async Task<EquipmentModel> IEquipmentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var equipment = await GetEquipmentOrThrowIfNotFoundAsync(id, cancellationToken);
        
        return mapper.Map<EquipmentModel>(equipment);
    }

    async Task<IReadOnlyCollection<EquipmentModel>> IEquipmentService.GetAllAsync(CancellationToken cancellationToken)
    {
        var equipment = await equipmentReadRepository.GetAllAsync(cancellationToken);
        return mapper.Map<IReadOnlyCollection<EquipmentModel>>(equipment);
    }

    async Task<EquipmentModel> IEquipmentService.CreateAsync(EquipmentCreateModel model, CancellationToken cancellationToken)
    {
        var result = mapper.Map<Equipment>(model);
        
        equipmentWriteRepository.Add(result);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<EquipmentModel>(result);
    }

    async Task<EquipmentModel> IEquipmentService.EditAsync(Guid id, EquipmentCreateModel model, CancellationToken cancellationToken)
    {
        var equipment = await GetEquipmentOrThrowIfNotFoundAsync(id,cancellationToken);
        
        equipment.Name = model.Name;
        equipment.ManufacturedYear = model.ManufacturedYear;
        equipment.SerialNumber = model.SerialNumber;
        
        equipmentWriteRepository.Update(equipment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<EquipmentModel>(equipment);
    }

    async Task IEquipmentService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var equipment = await GetEquipmentOrThrowIfNotFoundAsync(id,cancellationToken);
        equipmentWriteRepository.Delete(equipment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Метод для выброса ошибки если не удалось найти id
    /// </summary>
    private async Task<Equipment> GetEquipmentOrThrowIfNotFoundAsync(Guid id, CancellationToken cancellationToken)
    {
        var equipment = await equipmentReadRepository.GetByIdAsync(id, cancellationToken) ??
                       throw new EquipHandoverNotFoundException($"Не удалось найти документ с идентификатором {id}");
        return equipment;
    }
}