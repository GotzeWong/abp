using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Identity
{
    public interface IIdentityRoleAppService
        : ICrudAppService<
            IdentityRoleDto,
            Guid,
            GetIdentityRolesInput,
            IdentityRoleCreateDto,
            IdentityRoleUpdateDto>
    {
        Task<ListResultDto<IdentityRoleDto>> GetAllListAsync();
        Task<PagedResultDto<IdentityRoleDto>> SearchListAsync(IdentityRolePagedListDto input);
        Task BatchCreateAsync(Guid? tenantId, List<IdentityRoleExcelDto> roles);
        Task<MemoryStream> DownloadIdentityRoleAsync(IdentityRolePagedListDto input);
    }
}
