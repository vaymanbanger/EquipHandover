using AutoMapper;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Extensions;

namespace EquipHandover.Services;

/// <inheritdoc cref="IDocumentService"/>
public class DocumentService : IDocumentService, IServiceAnchor
{
    private readonly IMapper mapper;
    private readonly IDocumentReadRepository documentReadRepository;
    private readonly IDocumentWriteRepository documentWriteRepository;
    private readonly IUnitOfWork unitOfWork;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentService"/>
    /// </summary>
    public DocumentService(IMapper mapper,
        IDocumentReadRepository documentReadRepository,
        IDocumentWriteRepository documentWriteRepository,
        IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.documentReadRepository = documentReadRepository;
        this.unitOfWork = unitOfWork;
        this.documentWriteRepository = documentWriteRepository;
    }
    
    async Task<IReadOnlyCollection<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await documentReadRepository.GetAllAsync(cancellationToken);
        return mapper.Map<IReadOnlyCollection<DocumentModel>>(items);
    }

    async Task<DocumentModel> IDocumentService.CreateAsync(DocumentCreateModel model, CancellationToken cancellationToken)
    {
        var result = new Document()
        {
            Id = Guid.NewGuid(),
            RentalDate = model.RentalDate,
            SignatureNumber = model.SignatureNumber,
            City = model.City,
            ReceiverId = model.ReceiverId,
            SenderId = model.SenderId,
            DocumentEquipments = model.EquipmentIds
                .Select(equipmentId => new DocumentEquipment
                {
                    EquipmentId = equipmentId
                }).ToList()
        };
        documentWriteRepository.Add(result);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<DocumentModel>(result);
    }

    async Task<DocumentModel> IDocumentService.EditAsync(DocumentModel model, CancellationToken cancellationToken)
    {
        var entity = await documentReadRepository.GetByIdAsync(model.Id, cancellationToken);
        if (entity == null) 
            throw new EquipHandoverNotFoundException($"Не удалось найти документ с идентификатором {model.Id}");
        
        entity.RentalDate = model.RentalDate;
        entity.SignatureNumber = model.SignatureNumber;
        entity.City = model.City;
        
        documentWriteRepository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<DocumentModel>(entity);
    }
}