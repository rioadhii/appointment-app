using System.Linq.Dynamic.Core;
using Appointment.Core.Dto;
using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Base;
using Appointment.Data.Models;
using Appointment.Data.Repositories.Appointment;
using Appointment.Utils.Auth.UserInfo;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Appointment.Utils.Extensions;

namespace Appointment.Core.Services.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;
    private readonly IUserInfo _userInfo;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IMapper mapper,
        IUserInfo userInfo
    )
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
        _userInfo = userInfo;
    }

    public async Task<ResponseResultDto<bool>> CheckAvailability(AgentAvailabilityCheckInputDto input)
    {
        var response = new ResponseResultDto<bool>();
        try
        {
            var data = await _appointmentRepository.ValidateExistsAsync(new Appointments()
            {
                AgentId = input.AgentId,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                Date = input.Date
            });

            response.Data = !data;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = ex.GetStatusCode();
        }

        return response;
    }

    public async Task<ResponseResultDto<PagedListResult<AgentScheduleResultDto>>> GetAgentSchedule(
        AgentScheduleFilterDto input)
    {
        var actor = _userInfo.GetUserInfo();

        var data = await _appointmentRepository.GetAsync(actor.UserType, actor.UserId);
        List<AgentScheduleResultDto> mappedData = _mapper.MapFrom<List<AgentScheduleResultDto>>(data);

        var query = mappedData.AsQueryable()
            .OrderBy(input.Sorting)
            .WhereIf(
                !String.IsNullOrEmpty(input.Keyword),
                w => w.Customer.FullName.ToLower().Contains(input.Keyword.ToLower()));

        var result = query.ToPagedListResult(input.PageNumber, input.PageLength);

        return new ResponseResultDto<PagedListResult<AgentScheduleResultDto>>()
        {
            Data = result
        };
    }

    public async Task<ResponseResultDto<DetailAppointmentResultDto>> GetById(DetailAppointmentFilterDto input)
    {
        var data = await _appointmentRepository.GetByIdAsync(input.AppointmentId);

        if (data == null)
        {
            return new ResponseResultDto<DetailAppointmentResultDto>()
            {
                Success = false,
                StatusCode = (int)ResultCodeEnum.DATA_NOT_FOUND,
                Message = AppConsts.ApiResultNotFoundMessage
            };
        }
        
        DetailAppointmentResultDto mappedData = _mapper.MapFrom<DetailAppointmentResultDto>(data);

        return new ResponseResultDto<DetailAppointmentResultDto>()
        {
            Data = mappedData
        };
    }
}