using Appointment.Utils.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Appointment.Utils.Extensions;

public static class ResponseHandlerExtensions
{
    public static void UseJsonResponseHandler(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await next();

                context.Response.Body = originalBodyStream;

                if (context.Response.StatusCode == StatusCodes.Status204NoContent)
                {
                    return;
                }

                var response = new ApiResponse<object>
                {
                    Code = context.Response.StatusCode,
                    Path = context.Request.Path,
                    Timestamp = DateTime.UtcNow,
                    Message = "Successfully.",
                    Data = null,
                    Errors = null
                };

                if (context.Response.ContentType == "application/json")
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var json = await new StreamReader(responseBody).ReadToEndAsync();
                    response.Data = JsonConvert.DeserializeObject<object>(json);
                }

                context.Response.ContentType = "application/json";
                var jsonResponse = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        });
    }
}