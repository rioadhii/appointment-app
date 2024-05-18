namespace Appointment.Core.Dto.Base;

public class ResponseResultDto<T> : ResponseDto
{
    public T Data { get; set; }
}