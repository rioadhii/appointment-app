using Appointment.Utils.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Appointment.Utils.Filters;

public class ModelStateValidationFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is BadRequestObjectResult badRequestObjectResult)
        {
            if (badRequestObjectResult.Value is ValidationProblemDetails)
            {
                // Extracting error messages
                var errors = (badRequestObjectResult.Value as ValidationProblemDetails)?.Errors
                    .SelectMany(e => e.Value)
                    .Distinct()
                    .ToList();

                // Creating ApiResponse with error message
                var requestContext = context.HttpContext.Request;
                var response = new ApiResponse<object>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Path = $"{requestContext.Scheme}://{requestContext.Host}{requestContext.Path}",
                    Timestamp = DateTime.UtcNow,
                    Message = "Validation errors",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }

        base.OnResultExecuting(context);
    }
}
