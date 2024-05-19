using Appointment.Core.Dto;
using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Base;
using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Appointment.Data.Repositories.Appointment;
using Appointment.Utils.Extensions;

namespace Appointment.Core.Services.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly AppDbContext _db;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public AppointmentService(
        AppDbContext db,
        IAppointmentRepository appointmentRepository,
        IMapper mapper
    )
    {
        _db = db;
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
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
}