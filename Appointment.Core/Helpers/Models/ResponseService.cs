using Appointment.Utils.Extensions;

namespace Appointment.Core.Helpers.Models;

public class ResponseService: ResponseResult
{
    public ResponseService(string message, bool success, object result) : base(message, success, result)
    {
    }
}