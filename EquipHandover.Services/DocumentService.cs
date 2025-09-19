using AutoMapper;
using EquipHandover.Context.Contracts;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.Models;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Repositories.Contracts.WriteRepositories;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Export;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Services.Extensions;

namespace EquipHandover.Services;

/// <inheritdoc cref="IDocumentService"/>
public class DocumentService : IDocumentService, IServiceAnchor
{
    private readonly IMapper mapper;
    private readonly IDocumentReadRepository documentReadRepository;
    private readonly IDocumentWriteRepository documentWriteRepository;
    private readonly IEquipmentReadRepository equipmentReadRepository;
    private readonly IReceiverReadRepository receiverReadRepository;
    private readonly ISenderReadRepository senderReadRepository;
    private readonly IDocumentEquipmentReadRepository documentEquipmentReadRepository;
    private readonly IDocumentEquipmentWriteRepository documentEquipmentWriteRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IExcelService excelService;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentService"/>
    /// </summary>
    public DocumentService(IMapper mapper,
        IDocumentReadRepository documentReadRepository,
        IDocumentWriteRepository documentWriteRepository,
        IEquipmentReadRepository equipmentReadRepository,
        IReceiverReadRepository receiverReadRepository,
        ISenderReadRepository senderReadRepository,
        IDocumentEquipmentReadRepository documentEquipmentReadRepository,
        IDocumentEquipmentWriteRepository documentEquipmentWriteRepository,
        IUnitOfWork unitOfWork, 
        IExcelService excelService)
    {
        this.mapper = mapper;
        this.documentReadRepository = documentReadRepository;
        this.unitOfWork = unitOfWork;
        this.excelService = excelService;
        this.documentWriteRepository = documentWriteRepository;
        this.receiverReadRepository = receiverReadRepository;
        this.senderReadRepository = senderReadRepository;
        this.equipmentReadRepository = equipmentReadRepository;
        this.documentEquipmentReadRepository = documentEquipmentReadRepository;
        this.documentEquipmentWriteRepository = documentEquipmentWriteRepository;
    }

    async Task<DocumentModel> IDocumentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var document = await GetDocumentOrThrowIfNotFoundAsync(id, cancellationToken);
        return mapper.Map<DocumentModel>(document);
    }

    async Task<IReadOnlyCollection<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
    {
        var documents = await documentReadRepository.GetAllAsync(cancellationToken);
        return mapper.Map<IReadOnlyCollection<DocumentModel>>(documents);
    }

    async Task<DocumentModel> IDocumentService.CreateAsync(DocumentCreateModel model,
        CancellationToken cancellationToken)
    {
        var linkedEntities = await ThrowIfNotFoundLinkedEntitiesAsync(model, cancellationToken);
        
        var result = new Document
        {
            Id = Guid.NewGuid(),
            RentalDate = model.RentalDate,
            SignatureNumber = model.SignatureNumber,
            City = model.City,
            ReceiverId = model.ReceiverId,
            SenderId = model.SenderId,
        };
        
        documentWriteRepository.Add(result);

        foreach (var equipmentId in model.EquipmentIds)
        {
            var documentEquipment = new DocumentEquipment
            {
                EquipmentId = equipmentId,
                DocumentId = result.Id,
            };
            documentEquipmentWriteRepository.Add(documentEquipment);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        result.Receiver = linkedEntities.Receiver;
        result.Sender = linkedEntities.Sender;
        
        var document = mapper.Map<DocumentModel>(result);
        document.Equipment = mapper.Map<List<EquipmentModel>>(linkedEntities.Equipment);
        return document;
    }

    async Task<DocumentModel> IDocumentService.EditAsync(Guid id, DocumentCreateModel model, CancellationToken cancellationToken)
    {
        var documentDbModel = await GetDocumentOrThrowIfNotFoundAsync(id,cancellationToken);
        var linkedEntities = await ThrowIfNotFoundLinkedEntitiesAsync(model, cancellationToken);
        var dbDocumentEquipments = 
            await documentEquipmentReadRepository.GetByDocumentIdAsync(id, cancellationToken);
        var dbDocumentEquipmentIds = dbDocumentEquipments.Select(eq => eq.EquipmentId).ToList();
        var document = mapper.Map<Document>(documentDbModel);
        
        var equipmentToDeleteIds = dbDocumentEquipmentIds
            .Except(model.EquipmentIds)
            .ToList();
        var equipmentToAddIds = model.EquipmentIds
            .Except(dbDocumentEquipmentIds)
            .ToList();

        var equipmentToDelete = 
            await documentEquipmentReadRepository.GetByEquipmentIdsAsync(equipmentToDeleteIds, cancellationToken);
        foreach (var toDelete in equipmentToDelete)
        {
            documentEquipmentWriteRepository.Delete(toDelete);
        }

        foreach (var toAddId in equipmentToAddIds)
        {
            var documentEquipment = new DocumentEquipment
            {
                EquipmentId = toAddId,
                DocumentId = id
            };
            documentEquipmentWriteRepository.Add(documentEquipment);
        }
        
        document.RentalDate = model.RentalDate;
        document.SignatureNumber = model.SignatureNumber;
        document.City = model.City;
        document.ReceiverId = model.ReceiverId;
        document.SenderId = model.SenderId;
        
        documentWriteRepository.Update(document);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        document.Receiver = linkedEntities.Receiver;
        document.Sender = linkedEntities.Sender;
        
        var result = mapper.Map<DocumentModel>(document);
        result.Equipment = mapper.Map<List<EquipmentModel>>(linkedEntities.Equipment);
        return result;
    }

    
    async Task IDocumentService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var documentDbModel = await GetDocumentOrThrowIfNotFoundAsync(id,cancellationToken);
        var documentEquipments = 
            await documentEquipmentReadRepository.GetByDocumentIdAsync(id, cancellationToken);
        
        foreach (var toDelete in documentEquipments)
        {
            documentEquipmentWriteRepository.Delete(toDelete);
        }
        var document = mapper.Map<Document>(documentDbModel);
        
        documentWriteRepository.Delete(document);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    async Task<Stream> IDocumentService.ExportByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var document = await documentReadRepository.GetByIdAsync(id, cancellationToken) ?? 
                       throw new EquipHandoverNotFoundException($"Не удалось найти документ с идентификатором {id}");
        
        var result = mapper.Map<DocumentModel>(document);
        return excelService.Export(result, cancellationToken);
    }

    /// <summary>
    /// Метод для выброса ошибки если не удалось найти id по модели
    /// </summary>
    private async Task<ApplicationLinkedEntites> ThrowIfNotFoundLinkedEntitiesAsync(DocumentCreateModel model,
        CancellationToken cancellationToken)
    {
        var receiver = await receiverReadRepository.GetByIdAsync(model.ReceiverId, cancellationToken) ??
            throw new EquipHandoverNotFoundException(
                $"Не удалось найти принимающего с идентификатором {model.ReceiverId}");
        var sender = await senderReadRepository.GetByIdAsync(model.SenderId, cancellationToken) ??
            throw new EquipHandoverNotFoundException(
                $"Не удалось найти отправителя с идентификатором {model.SenderId}");
        var equipment = await equipmentReadRepository.GetByIdsAsync(model.EquipmentIds, cancellationToken);
        if (equipment.Count != model.EquipmentIds.Count)
        {
            throw new EquipHandoverNotFoundException(
                $"Не удалось найти оборудование с идентификатором  {string.Join(", ", model.EquipmentIds)}");
        }

        return new ApplicationLinkedEntites
        {
            Receiver = receiver,
            Sender = sender,
            Equipment = equipment
        };
    }

    /// <summary>
    /// Метод для выброса ошибки если не удалось найти id
    /// </summary>
    private async Task<DocumentDbModel> GetDocumentOrThrowIfNotFoundAsync(Guid id, CancellationToken cancellationToken)
    {
        var document = await documentReadRepository.GetByIdAsync(id, cancellationToken) ??
                       throw new EquipHandoverNotFoundException($"Не удалось найти документ с идентификатором {id}");
        return document;
    }
}