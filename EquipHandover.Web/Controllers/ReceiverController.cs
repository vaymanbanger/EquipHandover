using AutoMapper;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Models.Receiver;
using EquipHandover.Web.Infrastructure.Exceptions;
using EquipHandover.Web.Models.Receiver;
using Microsoft.AspNetCore.Mvc;

namespace EquipHandover.Web.Controllers;

/// <summary>
/// CRUD контроллер по работе с получателем
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReceiverController : ControllerBase
{
    private readonly IReceiverService receiverService;
    private readonly IValidateService validateService;
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ReceiverController"/>
    /// </summary>
    public ReceiverController(IReceiverService receiverService,
        IValidateService validateService,
        IMapper mapper)
    {
        this.receiverService = receiverService;
        this.validateService = validateService;
        this.mapper = mapper;
    }
    
    /// <summary>
    /// Получает список всех принимающих
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ReceiverResponseApiModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await receiverService.GetAllAsync(cancellationToken);
        
        return Ok(mapper.Map<IReadOnlyCollection<ReceiverResponseApiModel>>(result));
    }
    
    /// <summary>
    /// Добавляет нового принимающего
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ReceiverResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] ReceiverRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<ReceiverCreateModel>(request);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await receiverService.CreateAsync(requestModel, cancellationToken);
        
        return Ok(mapper.Map<ReceiverResponseApiModel>(result));
    }
    
    /// <summary>
    /// Редактирует существующего принимающего по идентификатору
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ReceiverResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] ReceiverRequestApiModel request,
        CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<ReceiverCreateModel>(request);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await receiverService.EditAsync(id, requestModel, cancellationToken);
        
        return Ok(mapper.Map<ReceiverResponseApiModel>(result));
    }
    
    /// <summary>
    /// Удаляет принимающего по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await receiverService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}