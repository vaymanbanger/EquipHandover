using AutoMapper;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Models.Sender;
using EquipHandover.Web.Infrastructure.Exceptions;
using EquipHandover.Web.Models.Sender;
using Microsoft.AspNetCore.Mvc;

namespace EquipHandover.Web.Controllers;

/// <summary>
/// CRUD контроллер по работе с отправителем
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SenderController : ControllerBase
{
    private readonly ISenderService senderService;
    private readonly IValidateService validateService;
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SenderController"/>
    /// </summary>
    public SenderController(ISenderService senderService,
        IValidateService validateService,
        IMapper mapper)
    {
        this.senderService = senderService;
        this.validateService = validateService;
        this.mapper = mapper;
    }
    
    /// <summary>
    /// Получает список всех отправителей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<SenderResponseApiModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await senderService.GetAllAsync(cancellationToken);
        
        return Ok(mapper.Map<IReadOnlyCollection<SenderResponseApiModel>>(result));
    }
    
    /// <summary>
    /// Добавляет нового отправителя
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SenderResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] SenderRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<SenderCreateModel>(request);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await senderService.CreateAsync(requestModel, cancellationToken);
        
        return Ok(mapper.Map<SenderResponseApiModel>(result));
    }
    
    /// <summary>
    /// Редактирует существующего отправителя по идентификатору
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SenderResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] SenderRequestApiModel request,
        CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<SenderCreateModel>(request);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await senderService.EditAsync(id, requestModel, cancellationToken);
        
        return Ok(mapper.Map<SenderResponseApiModel>(result));
    }
    
    /// <summary>
    /// Удаляет отправителя по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await senderService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}