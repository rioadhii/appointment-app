using Appointment.Core.Dto.Auth;
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
            c.CreateMap<UserCredentials, UserResultDto>()
                .ForMember(f => f.FirstName, act => act.MapFrom(src => src.User.FirstName))
                .ForMember(f => f.LastName, act => act.MapFrom(src => src.User.LastName))
                .ForMember(f => f.UserId, act => act.MapFrom(src => src.UserId))
                .ForMember(f => f.UserType, act => act.MapFrom(src => src.User.UserType));

            c.CreateMap<UserCredentials, TokenInputDto>()
                .ForMember(f => f.FirstName, act => act.MapFrom(src => src.User.FirstName))
                .ForMember(f => f.UserType, act => act.MapFrom(src => src.User.UserType));
            c.CreateMap<Users, UserResultDto>();
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