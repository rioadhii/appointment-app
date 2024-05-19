using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Common;
using Appointment.Core.Dto.Token;
using Appointment.Data.Models;
using AutoMapper;

namespace Appointment.Core.Dto;

public interface IMapper
{
    T MapFrom<T>(object entity);
    TDestination Map<TDestination, TSource>(TSource entity);
}

public class Mapper : IMapper
{
    private MapperConfiguration _mapper;

    public Mapper()
    {
        _mapper = new MapperConfiguration(c =>
        {
            c.CreateMap<Appointments, AgentScheduleResultDto>()
                .ForMember(f => f.AppointmentId, act => act.MapFrom(src => src.Id))
                .ForMember(f => f.Customer, act => act.MapFrom(src => src.Customer));

            c.CreateMap<Appointments, DetailAppointmentResultDto>()
                .ForMember(f => f.AppointmentId, act => act.MapFrom(src => src.Id))
                .ForMember(f => f.Customer, act => act.MapFrom(src => src.Customer))
                .ForMember(f => f.Agent, act => act.MapFrom(src => src.Agent));

            c.CreateMap<UserCredentials, UserResultDto>()
                .ForMember(f => f.FirstName, act => act.MapFrom(src => src.User.FirstName))
                .ForMember(f => f.LastName, act => act.MapFrom(src => src.User.LastName))
                .ForMember(f => f.UserId, act => act.MapFrom(src => src.UserId))
                .ForMember(f => f.UserType, act => act.MapFrom(src => src.User.UserType));

            c.CreateMap<UserCredentials, TokenInputDto>()
                .ForMember(f => f.FirstName, act => act.MapFrom(src => src.User.FirstName))
                .ForMember(f => f.UserType, act => act.MapFrom(src => src.User.UserType));
            c.CreateMap<Users, UserResultDto>();
            
            c.CreateMap<Users, AgentResultDto>()
                .ForMember(f => f.AgentId, act => act.MapFrom(src => src.Id))
                .ForMember(f => f.Email, act => act.MapFrom(src => src.UsersCredentials != null ? src.UsersCredentials.FirstOrDefault().Email : null))
                .ForMember(f => f.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                .ForMember(f => f.FullName, act => act.MapFrom(src => $"{src.FirstName} {(src.LastName != null ? src.LastName : "")}"));
            c.CreateMap<Users, CustomerResultDto>()
                .ForMember(f => f.CustomerId, act => act.MapFrom(src => src.Id))
                .ForMember(f => f.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                .ForMember(f => f.FullName, act => act.MapFrom(src => $"{src.FirstName} {(src.LastName != null ? src.LastName : "")}"));
        });
    }

    public TDestination Map<TDestination, TSource>(TSource entity)
    {
        return _mapper.CreateMapper().Map<TDestination>(entity);
    }

    public T MapFrom<T>(object entity)
    {
        return _mapper.CreateMapper().Map<T>(entity);
    }
}