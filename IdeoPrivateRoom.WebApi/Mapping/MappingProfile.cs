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
            .ForMember(u => u.UserName, conf => conf.MapFrom(scr => $"{scr.FirstName} {scr.LastName}"));

        CreateMap<RoleDto, RoleResponse>();
        CreateMap<UserRoleMapping, UserRoleMappingDto>();

        CreateMap<VocationRequest, VocationRequestDto>();
        CreateMap<VocationRequestDto, VocationRequest>();

    }
}
