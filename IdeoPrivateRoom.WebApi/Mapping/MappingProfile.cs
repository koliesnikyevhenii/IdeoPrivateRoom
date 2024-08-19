using AutoMapper;
using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;

namespace IdeoPrivateRoom.WebApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<UserRequest, UserDto>();
        CreateMap<UserDto, UserResponse>()
            .ForMember(u => u.Name, conf => conf.MapFrom(scr => $"{scr.FirstName} {scr.LastName}"))
            .ForMember(u => u.Icon, conf => conf.MapFrom(scr => scr.UserIcon));

        CreateMap<RoleDto, RoleResponse>();
        CreateMap<UserRoleMapping, UserRoleMappingDto>();

        CreateMap<VocationRequest, VocationRequestDto>();
        CreateMap<VocationRequestDto, VocationRequest>();

        CreateMap<VocationRequestDto, VocationResponse>()
            .ForMember(r => r.Start, conf => conf.MapFrom(scr => scr.StartDate))
            .ForMember(r => r.End, conf => conf.MapFrom(scr => scr.EndDate))
            .ForMember(r => r.Status, conf => conf.MapFrom(scr => scr.VocationStatus));

    }
}
