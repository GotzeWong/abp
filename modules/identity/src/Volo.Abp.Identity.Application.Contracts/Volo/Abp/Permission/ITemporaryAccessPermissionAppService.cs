using AgentHub.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Identity
{
    public interface ITemporaryAccessPermissionAppService : IApplicationService
    {
        Task<TemporaryAccessPermissionDto> CreateAsync(TemporaryAccessPermissionCreateDto input);
        Task<TemporaryAccessPermissionDto> UpdateAsync(Guid id, TemporaryAccessPermissionUpdateDto input);
        Task<PagedResultDto<TemporaryAccessPermissionDto>> GetListAsync(TemporaryAccessPermissionPagedListDto input);
        Task<TemporaryAccessPermissionDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<List<Guid>> GetAllByTemporaryUserIdAsync(Guid id);
    }
}
