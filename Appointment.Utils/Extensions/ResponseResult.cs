namespace Appointment.Utils.Extensions;

public class ResponseResult
{
    public string Message { get; set; }
    public bool Success { get; set; }
    public object Result { get; set; }
    public ResponseResult(string message, bool success, object? result) {
        Message = message;
        Success = success;
        Result = result;
    }
}