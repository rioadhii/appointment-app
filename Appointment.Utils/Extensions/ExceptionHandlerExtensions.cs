using System.Net;
using System.Security.Authentication;
using Appointment.Utils.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Appointment.Utils.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseJsonExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature != null)
                {
                    var error = errorFeature.Error;
                    var statusCode = GetStatusCode(error);

                    var response = new ApiResponse<object>
                    {
                        Code = statusCode,
                        Path = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}",
                        Timestamp = DateTime.UtcNow,
                        Message = (statusCode == StatusCodes.Status500InternalServerError)
                            ? "Internal Server Error: An unexpected error occurred."
                            : ((HttpStatusCode)statusCode).ToString(),
                        Data = null,
                        Errors = GetErrors(error)
                    };

                    context.Response.StatusCode = statusCode;
                    var jsonSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response, jsonSettings));
                }
            });
        });

        return app;
    }

    private static int GetStatusCode(Exception error)
    {
        return error switch
        {
            ArgumentNullException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            AuthenticationException => StatusCodes.Status401Unauthorized,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static IEnumerable<string> GetErrors(Exception error)
    {
        return new List<string> { error.Message };
    }
}