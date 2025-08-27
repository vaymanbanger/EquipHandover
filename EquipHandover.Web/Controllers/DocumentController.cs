using AutoMapper;
using EquipHandover.Services.Contracts;
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
    private readonly IMapper mapper;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DocumentController"/>
    /// </summary>
    public DocumentController(IDocumentService documentService,
        IMapper mapper,
        IValidateService validateService)
    {
        this.documentService = documentService;
        this.mapper = mapper;
        this.validateService = validateService;
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
    public async Task<IActionResult> Create(DocumentRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<DocumentCreateModel>(request);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await documentService.CreateAsync(requestModel, cancellationToken);
        
        return Ok(mapper.Map<DocumentResponseApiModel>(result));
    }

    /// <summary>
    /// Редактирует существующий документ
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DocumentResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
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