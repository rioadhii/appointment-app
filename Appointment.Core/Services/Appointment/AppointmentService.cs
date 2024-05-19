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

    public async Task<ResponseResultDto<CreateAppointmentResultDto>> Book(CreateAppointmentInputDto input)
    {
        var result = new ResponseResultDto<CreateAppointmentResultDto>();
        var actor = _userInfo.GetUserInfo();

        try
        {
            if (actor.UserType == UserType.Agent)
            {
                result.Success = false;
                result.StatusCode = (int)ResultCodeEnum.FORBIDDEN;
                result.Message = AppConsts.ApiResultOperationNotPermitted;

                return result;
            }

            var endTime = input.StartTime.Add(TimeSpan.FromMinutes(input.Duration));

            var isAgentHasAppointment = await _appointmentRepository.ValidateExistsAsync(new Appointments()
            {
                AgentId = input.AgentId,
                StartTime = input.StartTime,
                EndTime = endTime,
                Date = input.Date
            });

            if (isAgentHasAppointment)
            {
                result.Success = false;
                result.StatusCode = (int)ResultCodeEnum.BAD_REQUEST;
                result.Message = "Agent already has appointment";
            }
            else
            {
                var newAppointment = _mapper.MapFrom<Appointments>(input);
                newAppointment.CustomerId = actor.UserId;
                newAppointment.EndTime = endTime;
                var appointment = await _appointmentRepository.AddAsync(newAppointment);
                result.Data = new CreateAppointmentResultDto()
                {
                    AppointmentId = appointment.Id
                };
            }
        }
        catch (Exception ex)
        {
            result.StatusCode = ex.GetStatusCode();
            result.Message = ex.Message;
            result.Success = false;
        }

        return result;
    }

    public async Task<ResponseResultDto<PagedListResult<AgentScheduleResultDto>>> GetAgentSchedule(
        AppointmentScheduleFilterDto input)
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
        var actor = _userInfo.GetUserInfo();
        var data = await _appointmentRepository.GetByIdAsync(input.AppointmentId, actor.UserId);

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

    public async Task<ResponseResultDto<PagedListResult<CustomerScheduleResultDto>>> GetCustomerSchedule(
        AppointmentScheduleFilterDto input
    )
    {
        var actor = _userInfo.GetUserInfo();

        var data = await _appointmentRepository.GetAsync(actor.UserType, actor.UserId);
        List<CustomerScheduleResultDto> mappedData = _mapper.MapFrom<List<CustomerScheduleResultDto>>(data);

        var query = mappedData.AsQueryable()
            .OrderBy(input.Sorting)
            .WhereIf(
                !String.IsNullOrEmpty(input.Keyword),
                w => w.Agent.FullName.ToLower().Contains(input.Keyword.ToLower()));

        var result = query.ToPagedListResult(input.PageNumber, input.PageLength);

        return new ResponseResultDto<PagedListResult<CustomerScheduleResultDto>>()
        {
            Data = result
        };
    }
}