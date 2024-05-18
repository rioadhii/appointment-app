using System.Net;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Utils.Helpers;

public static class ApiResponseHelper
{
    public static IActionResult FormatResponse<T>(
        ControllerBase controller, 
        T data,
        int statusCode = 200,
        string message = AppConsts.ApiSuccessMessage)
    {
        var context = controller.HttpContext;
        var apiResponse = new ApiResponse<T>
        {
            Code = statusCode,
            Path = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}",
            Timestamp = DateTime.UtcNow,
            Message = statusCode == (int)HttpStatusCode.InternalServerError ? "Internal Server Error" : message,
            Data = data,
            Errors = null
        };

        return new OkObjectResult(apiResponse)
        {
            StatusCode = statusCode
        };
    }
}