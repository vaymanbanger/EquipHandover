using AutoMapper;
using EquipHandover.Repositories.Contracts.ReadRepositories;
using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Services.Contracts.Export;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Extensions;

namespace EquipHandover.Services.Export;

/// <inheritdoc cref="IExportService"/>
public class ExportService : IExportService, IServiceAnchor
{
    private readonly IDocumentReadRepository documentReadRepository;
    private readonly IExcelService excelService;
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExportService"/> 
    /// </summary>
    public ExportService(IDocumentReadRepository documentReadRepository,
        IExcelService excelService,
        IMapper mapper)
    {
        this.documentReadRepository = documentReadRepository;
        this.excelService = excelService;
        this.mapper = mapper;
    }

    async Task<byte[]> IExportService.ExportByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var document = await documentReadRepository.GetByIdWithFullModelAsync(id, cancellationToken) ?? 
                       throw new EquipHandoverNotFoundException($"Не удалось найти документ с идентификатором {id}");
        
        var result = mapper.Map<DocumentModel>(document);
        return excelService.Export(result, cancellationToken);
    }
}