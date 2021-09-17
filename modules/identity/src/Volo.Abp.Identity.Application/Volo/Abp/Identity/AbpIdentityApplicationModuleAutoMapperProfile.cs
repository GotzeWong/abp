using AutoMapper;

namespace Volo.Abp.Identity
{
    public class AbpIdentityApplicationModuleAutoMapperProfile : Profile
    {
        public AbpIdentityApplicationModuleAutoMapperProfile()
        {
            CreateMap<IdentityUser, IdentityUserDto>()
                .MapExtraProperties();

            CreateMap<IdentityUser, IdentityUserLeadertDto>();

            CreateMap<IdentityUser, IdentityUserDetailDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationUnits, opt => opt.Ignore());

            CreateMap<IdentityUserDto, IdentityUserExcelDto>()
                .ForMember(dest => dest.RoleNames, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationUnitNames, opt => opt.Ignore())
                .ForMember(dest => dest.ChargeofOrgNames, opt => opt.Ignore());

            CreateMap<IdentityRole, IdentityRoleDto>()
                .MapExtraProperties();


            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();

            CreateMap<OrganizationUnit, OrganizationUnitParentDto>()
                .ForMember(dest => dest.AllMembers, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.Leaders, opt => opt.Ignore());

            CreateMap<OrganizationUnit, OrganizationUnitChildDto>();

            CreateMap<IdentityUser, ProfileDto>()
                .ForMember(dest => dest.HasPassword,
                    op => op.MapFrom(src => src.PasswordHash != null))
                .MapExtraProperties();

            CreateMap<TemporaryAccessPermission, TemporaryAccessPermissionDto>()
                .ForMember(dest => dest.GrantUserId, opt => opt.MapFrom(s => s.GrantUser.Id))
                .ForMember(dest => dest.GrantUserName, opt => opt.MapFrom(s => s.GrantUser.Name))
                .ForMember(dest => dest.TemporaryUserId, opt => opt.MapFrom(s => s.TemporaryUser.Id))
                .ForMember(dest => dest.TemporaryUserName, opt => opt.MapFrom(s => s.TemporaryUser.Name));

        }
    }
}
