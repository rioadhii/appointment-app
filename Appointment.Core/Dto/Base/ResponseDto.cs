using Appointment.Utils.Constant;

namespace Appointment.Core.Dto.Base;

public class ResponseDto
{
    public string Message { get; set; }
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public ResponseDto() {
        Message = AppConsts.ApiSuccessMessage;
        Success = true;
        StatusCode = 200;
    }
}