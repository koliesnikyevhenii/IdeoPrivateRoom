using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;

namespace IdeoPrivateRoom.WebApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserEntity, VocationUserDto>();
        CreateMap<UserDto, UserEntity>();
        CreateMap<UserRequest, UserDto>();
        CreateMap<UserDto, UserResponse>()
            .ForMember(u => u.Name, conf => conf.MapFrom(scr => $"{scr.FirstName} {scr.LastName}"))
            .ForMember(u => u.Icon, conf => conf.MapFrom(scr => scr.UserIcon));

        CreateMap<RoleDto, RoleResponse>();
        CreateMap<UserRoleMappingEntity, UserRoleMappingDto>();

        CreateMap<VocationRequestEntity, VocationRequestDto>();
  
        CreateMap<VocationRequestDto, VocationRequestEntity>();
        CreateMap<VocationUserDto, VocationUserResponse>();
        CreateMap<VocationRequestDto, VocationResponse>();
        CreateMap<VocationUserApprovalDto, VocationUserApprovalResponse>();
        CreateMap<VocationRequestDto, VocationResponse>()
            .ForMember(r => r.Start, conf => conf.MapFrom(scr => scr.StartDate))
            .ForMember(r => r.End, conf => conf.MapFrom(scr => scr.EndDate))
            .ForMember(r => r.Status, conf => conf.MapFrom(scr => scr.VocationStatus));


        CreateMap<VocationRequestEntity, VocationRequestDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.UserApprovalResponses, opt => opt.MapFrom(src => src.UserApprovalResponses));

        CreateMap<UserEntity, VocationUserDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.UserIcon));

        CreateMap<UserApprovalResponseEntity, VocationUserApprovalDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => src.ApprovalStatus));

    }
}
