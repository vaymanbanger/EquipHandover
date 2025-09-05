using AutoMapper;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Export;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Web.Infrastructure.Exceptions;
using EquipHandover.Web.Models.Document;
using Microsoft.AspNetCore.Mvc;

namespace EquipHandover.Web.Controllers;
/// <summary>
/// CRUD контроллер по работе с документом
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService documentService;
    private readonly IValidateService validateService;
    private readonly IExportService exportService;
    private readonly IMapper mapper;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentController"/>
    /// </summary>
    public DocumentController(IDocumentService documentService,
        IMapper mapper,
        IValidateService validateService,
        IExportService exportService)
    {
        this.documentService = documentService;
        this.mapper = mapper;
        this.validateService = validateService;
        this.exportService = exportService;
    }

    /// <summary>
    /// Экспортирует документ по идентификатору
    /// </summary>
    [HttpGet("{id:guid}/export")]
    [ProducesResponseType(typeof(File),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExportById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var bytes = await exportService.ExportByIdAsync(id, cancellationToken);
        
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export.xlsx");
    }

    /// <summary>
    /// Получает список всех документов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<DocumentResponseApiModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await documentService.GetAllAsync(cancellationToken);
        
        return Ok(mapper.Map<IReadOnlyCollection<DocumentResponseApiModel>>(result));
    }

    /// <summary>
    /// Добавляет новый документ
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DocumentResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] DocumentRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<DocumentCreateModel>(request);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await documentService.CreateAsync(requestModel, cancellationToken);
        
        return Ok(mapper.Map<DocumentResponseApiModel>(result));
    }

    /// <summary>
    /// Редактирует существующий документ по идентификатору 
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DocumentResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] DocumentRequestApiModel request,
        CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<DocumentCreateModel>(request);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await documentService.EditAsync(id, requestModel, cancellationToken);
        
        return Ok(mapper.Map<DocumentResponseApiModel>(result));
    }
    
    /// <summary>
    /// Удаляет документ по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await documentService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}