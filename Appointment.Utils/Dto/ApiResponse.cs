using System.Text.Json.Serialization;

namespace Appointment.Utils.Dto;

public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Path { get; set; }
    public DateTime Timestamp { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    [JsonIgnore]
    public IEnumerable<string> Errors { get; set; }
}