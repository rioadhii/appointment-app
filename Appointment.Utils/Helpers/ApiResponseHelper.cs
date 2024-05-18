using Appointment.Utils.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Utils.Helpers;

public class ApiResponseHelper
{
    public static IActionResult CreateResponse<T>(
        HttpContext httpContext,
        T data,
        int statusCode = StatusCodes.Status200OK,
        string message = "Successfully",
        IEnumerable<string> errors = null
    )
    {
        var response = new ApiResponse<T>
        {
            Code = statusCode,
            Path = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}",
            Timestamp = DateTime.UtcNow,
            Message = message,
            Data = data,
            Errors = errors
        };

        return new ObjectResult(response) { StatusCode = statusCode };
    }
}