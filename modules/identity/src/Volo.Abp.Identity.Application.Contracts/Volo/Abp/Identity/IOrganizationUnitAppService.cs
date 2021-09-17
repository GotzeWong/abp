using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Identity
{
    public interface IOrganizationUnitAppService : ICrudAppService<OrganizationUnitDto, Guid, GetOrganizationUnitsInput, OrganizationUnitCreateDto, OrganizationUnitUpdateDto>
    {
        Task<List<OrganizationUnitDto>> GetAllListAsync();
        Task<List<OrganizationUnitMenuDto>> GetMenuListAsync();
        Task<List<OrganizationUnitParentDto>> GetTreeListAsync(Guid? parentId);
        Task<PagedResultDto<OrganizationUnitDto>> SearchListAsync(OrganizationUnitPagedListDto input);
        Task BatchCreateAsync(Guid? tenantId, List<OrganizationUnitExcelDto> orgs);
        Task<List<IdentityUserLeadertDto>> GetAllLeadersAsync(OrganizationUnit organizationUnit);
        Task<MemoryStream> DownloadOrganizationUnitAsync(OrganizationUnitPagedListDto input);
        Task BindAsync(OrganizationUnitBindDto input); 
        Task UnbindAsync(OrganizationUnitUnbindDto input);
        Task SetLeaderAsync(OrganizationUnitLeaderDto input);
    }
}
