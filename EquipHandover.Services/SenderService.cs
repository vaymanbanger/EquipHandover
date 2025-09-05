using AutoMapper;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Sender;
using EquipHandover.Services.Extensions;

namespace EquipHandover.Services;

/// <inheritdoc cref="ISenderService"/>
public class SenderService : ISenderService, IServiceAnchor
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ISenderReadRepository senderReadRepository;
    private readonly ISenderWriteRepository senderWriteRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderService"/>
    /// </summary>
    public SenderService(IMapper mapper,
        IUnitOfWork unitOfWork,
        ISenderReadRepository senderReadRepository,
        ISenderWriteRepository senderWriteRepository)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.senderReadRepository = senderReadRepository;
        this.senderWriteRepository = senderWriteRepository;
    }
    
    async Task<IReadOnlyCollection<SenderModel>> ISenderService.GetAllAsync(CancellationToken cancellationToken)
    {
        var senders = await senderReadRepository.GetAllAsync(cancellationToken);
        return mapper.Map<IReadOnlyCollection<SenderModel>>(senders);
    }

    async Task<SenderModel> ISenderService.CreateAsync(SenderCreateModel model, CancellationToken cancellationToken)
    {
        var result = new Sender
        {
            Id = Guid.NewGuid(),
            FullName = model.FullName,
            Enterprise = model.Enterprise,
            TaxPayerId = model.TaxPayerId
        };
        senderWriteRepository.Add(result);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<SenderModel>(result);
    }

    async Task<SenderModel> ISenderService.EditAsync(Guid id, SenderCreateModel model, CancellationToken cancellationToken)
    {
        var sender = await GetSenderOrThrowIfNotFoundAsync(id,cancellationToken);
        
        sender.TaxPayerId = model.TaxPayerId;
        sender.FullName = model.FullName;
        sender.Enterprise = model.Enterprise;
        
        senderWriteRepository.Update(sender);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<SenderModel>(sender);
    }

    async Task ISenderService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sender = await GetSenderOrThrowIfNotFoundAsync(id,cancellationToken);
        senderWriteRepository.Delete(sender);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Метод для выброса ошибки если не удалось найти id
    /// </summary>
    private async Task<Sender> GetSenderOrThrowIfNotFoundAsync(Guid id, CancellationToken cancellationToken)
    {
        var sender = await senderReadRepository.GetByIdAsync(id, cancellationToken) ??
                       throw new EquipHandoverNotFoundException($"Не удалось найти документ с идентификатором {id}");
        return sender;
    }
}