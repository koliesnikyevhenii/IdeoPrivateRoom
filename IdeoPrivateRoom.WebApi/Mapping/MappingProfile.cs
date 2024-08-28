using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Models;
using IdeoPrivateRoom.WebApi.Mapping.Converters;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;

namespace IdeoPrivateRoom.WebApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, UserRequest>()
            .ReverseMap();

        CreateMap<UserEntity, UserResponse>()
            .ForMember(u => u.Name, conf => conf.MapFrom(scr => $"{scr.FirstName} {scr.LastName}"))
            .ForMember(u => u.Icon, conf => conf.MapFrom(scr => scr.UserIcon))
            .ForMember(u => u.Roles, conf => conf.MapFrom(scr => scr.RoleMappings));

        CreateMap<UserEntity, VacationUserResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.UserIcon));

        CreateMap<RoleEntity, RoleResponse>()
            .ReverseMap();

        CreateMap<UserRoleMappingEntity, RoleResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name));

        CreateMap<VacationRequestEntity, VacationResponse>()
            .ForMember(r => r.Start, conf => conf.MapFrom(scr => scr.StartDate))
            .ForMember(r => r.End, conf => conf.MapFrom(scr => scr.EndDate))
            .ForMember(r => r.Status, conf => conf.MapFrom(scr => scr.VacationStatus));

        CreateMap<UserApprovalResponseEntity, VacationUserApprovalResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => int.Parse(src.ApprovalStatus).ToString()));

        CreateMap(typeof(PagedList<VacationRequestEntity>), typeof(PagedList<VacationResponse>))
            .ConvertUsing(typeof(VacationsPagedListConverter<VacationRequestEntity, VacationResponse>));

    }
}
