using AutoMapper;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Receiver;
using EquipHandover.Services.Extensions;

namespace EquipHandover.Services;

/// <inheritdoc cref="IReceiverService"/>
public class ReceiverService : IReceiverService, IServiceAnchor
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IReceiverReadRepository receiverReadRepository;
    private readonly IReceiverWriteRepository receiverWriteRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverService"/>
    /// </summary>
    public ReceiverService(IMapper mapper,
        IUnitOfWork unitOfWork,
        IReceiverReadRepository receiverReadRepository,
        IReceiverWriteRepository receiverWriteRepository)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.receiverReadRepository = receiverReadRepository;
        this.receiverWriteRepository = receiverWriteRepository;
    }
    
    async Task<IReadOnlyCollection<ReceiverModel>> IReceiverService.GetAllAsync(CancellationToken cancellationToken)
    {
        var receivers = await receiverReadRepository.GetAllAsync(cancellationToken);
        return mapper.Map<IReadOnlyCollection<ReceiverModel>>(receivers);
    }

    async Task<ReceiverModel> IReceiverService.CreateAsync(ReceiverCreateModel model, CancellationToken cancellationToken)
    {
        var result = new Receiver
        {
            Id = Guid.NewGuid(),
            FullName = model.FullName,
            Enterprise = model.Enterprise,
            RegistrationNumber = model.RegistrationNumber,
        };
        receiverWriteRepository.Add(result);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<ReceiverModel>(result);
    }

    async Task<ReceiverModel> IReceiverService.EditAsync(Guid id,ReceiverCreateModel model, CancellationToken cancellationToken)
    {
        var receiver = await GetReceiverOrThrowIfNotFoundAsync(id,cancellationToken);
        
        receiver.RegistrationNumber = model.RegistrationNumber;
        receiver.FullName = model.FullName;
        receiver.Enterprise = model.Enterprise;
        
        receiverWriteRepository.Update(receiver);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<ReceiverModel>(receiver);
    }

    async Task IReceiverService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var receiver = await GetReceiverOrThrowIfNotFoundAsync(id,cancellationToken);
        receiverWriteRepository.Delete(receiver);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Метод для выброса ошибки если не удалось найти id
    /// </summary>
    private async Task<Receiver> GetReceiverOrThrowIfNotFoundAsync(Guid id, CancellationToken cancellationToken)
    {
        var receiver = await receiverReadRepository.GetByIdAsync(id, cancellationToken) ??
                     throw new EquipHandoverNotFoundException($"Не удалось найти документ с идентификатором {id}");
        return receiver;
    }
}