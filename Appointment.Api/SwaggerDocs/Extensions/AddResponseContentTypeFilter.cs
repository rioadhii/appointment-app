using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Appointment.Api.SwaggerDocs.Extensions;

public class AddResponseContentTypeFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var responseContentType = "application/json"; // Specify your response content type here
        if (!operation.Responses.ContainsKey("200"))
            return;
        if (!operation.Responses["200"].Content.ContainsKey(responseContentType))
        {
            operation.Responses["200"].Content.Clear();
            operation.Responses["200"].Content.Add(responseContentType, new OpenApiMediaType());
        }
    }
}
