using AutoMapper;
using EquipHandover.Services.Contracts;
using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Web.Infrastructure.Exceptions;
using EquipHandover.Web.Models.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace EquipHandover.Web.Controllers;

/// <summary>
/// CRUD контроллер по работе с оборудованием
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService equipmentService;
    private readonly IValidateService validateService;
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="EquipmentController"/>
    /// </summary>
    public EquipmentController(IEquipmentService equipmentService,
        IValidateService validateService,
        IMapper mapper)
    {
        this.equipmentService = equipmentService;
        this.validateService = validateService;
        this.mapper = mapper;
    }
    
    /// <summary>
    /// Получает оборудование по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EquipmentResponseApiModel),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        var result =  await equipmentService.GetByIdAsync(id, cancellationToken);
        return Ok(mapper.Map<EquipmentResponseApiModel>(result));
    }
    
    /// <summary>
    /// Получает список всего оборудования
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<EquipmentResponseApiModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await equipmentService.GetAllAsync(cancellationToken);
        
        return Ok(mapper.Map<IReadOnlyCollection<EquipmentResponseApiModel>>(result));
    }
    
    /// <summary>
    /// Добавляет новое оборудование
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(EquipmentResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] EquipmentCreateApiModel create, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<EquipmentCreateModel>(create);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await equipmentService.CreateAsync(requestModel, cancellationToken);
        
        return Ok(mapper.Map<EquipmentResponseApiModel>(result));
    }
    
    /// <summary>
    /// Редактирует существующее оборудование по идентификатору
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EquipmentResponseApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EquipmentCreateApiModel create,
        CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<EquipmentCreateModel>(create);
        await validateService.ValidateAsync(requestModel, cancellationToken);
        var result = await equipmentService.EditAsync(id, requestModel, cancellationToken);
        
        return Ok(mapper.Map<EquipmentResponseApiModel>(result));
    }
    
    /// <summary>
    /// Удаляет оборудование по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await equipmentService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}