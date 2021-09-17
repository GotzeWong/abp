using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Identity
{
    public interface IIdentityUserAppService 
        : ICrudAppService<
            IdentityUserDto, 
            Guid, 
            GetIdentityUsersInput, 
            IdentityUserCreateDto, 
            IdentityUserUpdateDto>
    {
        Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id);
        Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync();

        Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input);

        Task<IdentityUserDto> FindByUsernameAsync(string userName);

        Task<IdentityUserDto> FindByEmailAsync(string email);

        Task<PagedResultDto<IdentityUserDetailDto>> SearchListAsync(IdentityUserPagedListDto input);

        Task BatchCreateAsync(Guid? tenantId, List<IdentityUserExcelDto> input);

        Task<IdentityUserDetailDto> VueCreateAsync(IdentityUserVueCreateDto input);
        Task<IdentityUserDetailDto> VueUpdateAsync(Guid id, IdentityUserVueUpdateDto input);
        Task<MemoryStream> DownloadIdentityUserAsync(IdentityUserPagedListDto input);
        Task<List<GrantedOrganizationDto>> GetGrantedOrganizationAsync(Guid id);

    }
}
