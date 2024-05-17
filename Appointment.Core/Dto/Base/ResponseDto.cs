namespace Appointment.Core.Dto.Base;

public class ResponseDto
{
    public string Message { get; set; }
    public bool Success { get; set; }

    public ResponseDto() {
        this.Message = "";
        this.Success = true;
    }
}