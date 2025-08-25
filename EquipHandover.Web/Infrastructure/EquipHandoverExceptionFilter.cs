using EquipHandover.Services.Contracts.Exceptions;
using EquipHandover.Web.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EquipHandover.Web.Infrastructure;

/// <summary>
/// Фильтр обработки ошибок
/// </summary>
public class EquipHandoverExceptionFilter : IExceptionFilter
{
    void IExceptionFilter.OnException(ExceptionContext context)
    {
        if (context.Exception is not EquipHandoverException exception)
        {
            return;
        }

        switch (exception) 
        {
            case EquipHandoverNotFoundException ex:
                SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail(ex.Message)), context);
                break;
            case EquipHandoverInvalidOperationException ex:
                SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(ex.Message))
                {
                    StatusCode = StatusCodes.Status406NotAcceptable
                }, context);
                break;
            case EquipHandoverValidationException ex:
                SetDataToContext(new BadRequestObjectResult(new ApiValidationExceptionDetail()
                {
                    Errors = ex.Errors
                })
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                }, context);
                break;
            default:
                SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(exception.Message)), context);
                break;
        }
    }

    private static void SetDataToContext(ObjectResult data, ExceptionContext context)
    {
        context.ExceptionHandled = true;
        var response = context.HttpContext.Response;
        response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
        context.Result = data;
    }
}