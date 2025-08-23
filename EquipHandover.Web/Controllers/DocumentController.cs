using AutoMapper;
using EquipHandover.Services.Contracts;
using EquipHandover.Web.Models.Document;
using Microsoft.AspNetCore.Mvc;

namespace EquipHandover.Web.Controllers;
/// <summary>
/// CRUD контроллер по работе с документом
/// </summary>
[ApiController]
[Route("[controller]")]
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
    /// Добавляет новый товар
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DocumentResponseApiModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(DocumentRequestApiModel request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}