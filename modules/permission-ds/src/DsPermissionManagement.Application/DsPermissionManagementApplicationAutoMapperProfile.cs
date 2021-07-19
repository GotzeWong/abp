using AutoMapper;
using DsPermissionManagement.DsPermissions;
using DsPermissionManagement.Permissions;

namespace DsPermissionManagement
{
    public class DsPermissionManagementApplicationAutoMapperProfile : Profile
    {
        public DsPermissionManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<DsPermission, DsPermissionDto>();
        }
    }
}