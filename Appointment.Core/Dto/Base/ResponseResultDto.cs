using Appointment.Utils.Constant;

namespace Appointment.Core.Dto.Base;

public class ResponseResultDto<T> : ResponseDto
{
    public T? Data { get; set; }

    public ResponseResultDto()
    {
        Message = AppConsts.ApiSuccessMessage;
        Success = true;
        StatusCode = 200;

        // Initialize Data property with default value for type T
        Data = default(T);
    }
}