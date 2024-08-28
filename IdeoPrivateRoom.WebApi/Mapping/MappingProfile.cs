using AutoMapper;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Models;
using IdeoPrivateRoom.WebApi.Extension;
using IdeoPrivateRoom.WebApi.Mapping.Converters;
using IdeoPrivateRoom.WebApi.Models.Enums;
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

        CreateMap<UserEntity, VocationUserResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.UserIcon));

        CreateMap<RoleEntity, RoleResponse>()
            .ReverseMap();

        CreateMap<UserRoleMappingEntity, RoleResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name));

        CreateMap<VocationRequestEntity, VocationResponse>()
            .ForMember(r => r.Start, conf => conf.MapFrom(scr => scr.StartDate))
            .ForMember(r => r.End, conf => conf.MapFrom(scr => scr.EndDate))
            .ForMember(r => r.Status, conf => conf.MapFrom(scr => scr.VocationStatus))
            .ForMember(r => r.Reviewers, conf => conf.MapFrom(scr => scr.User.LinkedUsers.Select(lu => new VocationReviewerResponse
            {
                Id = lu.AssociatedUser.Id,
                Name = lu.AssociatedUser.FirstName + " " + lu.AssociatedUser.LastName,
                Icon = lu.AssociatedUser.UserIcon,
                ApprovalStatus = lu.AssociatedUser.UserApprovalResponses.FirstOrDefault(ar => ar.VocationRequestId == scr.Id) != null 
                    ? lu.AssociatedUser.UserApprovalResponses.FirstOrDefault(ar => ar.VocationRequestId == scr.Id)!.ApprovalStatus.ToEnum(ApprovalStatus.Pending)
                    : ApprovalStatus.Pending
            }).ToList()));

        CreateMap<UserApprovalResponseEntity, VocationUserApprovalResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => int.Parse(src.ApprovalStatus).ToString()));

        CreateMap<UpdateVocationRequest, VocationRequestEntity>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Start))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.End))
            .ForMember(dest => dest.VocationStatus, opt => opt.MapFrom(src => src.Status));

        CreateMap(typeof(PagedList<VocationRequestEntity>), typeof(PagedList<VocationResponse>))
            .ConvertUsing(typeof(VocationsPagedListConverter<VocationRequestEntity, VocationResponse>));

    }
}
