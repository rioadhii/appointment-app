using Appointment.Utils.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Utils.Helpers;

public static class ApiResponseHelper
{
    public static IActionResult Ok<T>(
        ControllerBase controller, T data, string message = "Successfully")
    {
        var context = controller.HttpContext;
        var apiResponse = new ApiResponse<T>
        {
            Code = context.Response.StatusCode,
            Path = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}",
            Timestamp = DateTime.UtcNow,
            Message = message,
            Data = data,
            Errors = null
        };

        return new OkObjectResult(apiResponse);
    }

    // Overload for cases where no data needs to be passed
    public static IActionResult Ok(
        ControllerBase controller, string message = "Successfully")
    {
        var context = controller.HttpContext;
        var apiResponse = new ApiResponse<object>
        {
            Code = context.Response.StatusCode,
            Path = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}",
            Timestamp = DateTime.UtcNow,
            Message = message,
            Data = null,
            Errors = null
        };

        return new OkObjectResult(apiResponse);
    }
}