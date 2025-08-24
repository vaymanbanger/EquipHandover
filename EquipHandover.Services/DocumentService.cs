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
    public DocumentService(EquipHandoverContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    async Task<IReadOnlyCollection<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await context.Set<Entities.Document>()
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync(cancellationToken);
        
        return mapper.Map<IReadOnlyCollection<DocumentModel>>(items);
    }

    Task<DocumentModel> IDocumentService.CreateAsync(DocumentCreateModel documentCreateModel, CancellationToken cancellationToken)
    { 
        throw new NotImplementedException();
    }
    
}