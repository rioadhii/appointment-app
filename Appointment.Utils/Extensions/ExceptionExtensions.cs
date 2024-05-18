using System.Security.Authentication;

namespace Appointment.Utils.Extensions;

public static class ExceptionExtensions
{
    public static int GetStatusCode(this Exception ex)
    {
        // Define mapping between exception types and status codes
        var statusCodeMapping = new Dictionary<Type, int>
        {
            { typeof(AuthenticationException), 401 },  // Unauthorized
            // Add more mappings as needed
        };

        // Check if the exception type is mapped to a status code
        foreach (var mapping in statusCodeMapping)
        {
            if (mapping.Key.IsInstanceOfType(ex))
            {
                return mapping.Value;
            }
        }

        // Default status code for other exceptions
        return 500; // Internal Server Error
    }
}
