using AutoMapper;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Models.Document;

namespace EquipHandover.Services;

/// <inheritdoc cref="IDocumentService"/>
public class DocumentService : IDocumentService
{
    private readonly IMapper mapper;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentService"/>
    /// </summary>
    public DocumentService(IMapper mapper)
    {
        this.mapper = mapper;
    }
    
    Task<IReadOnlyCollection<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<DocumentModel> IDocumentService.CreateAsync(DocumentCreateModel documentCreateModel, CancellationToken cancellationToken)
    { 
        throw new NotImplementedException();
    }

    Task<DocumentModel> IDocumentService.EditAsync(DocumentModel model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}