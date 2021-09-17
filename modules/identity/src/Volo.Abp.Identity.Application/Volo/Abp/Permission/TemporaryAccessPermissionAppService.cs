using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity
{
    [Authorize(IdentityPermissions.TemporaryAccessPermissions.Default)]
    public class TemporaryAccessPermissionAppService : IdentityAppServiceBase, ITemporaryAccessPermissionAppService
    {
        protected ITemporaryAccessPermissionRepository TemporaryAccessPermissionRepository { get; }
        protected TemporaryAccessPermissionManager TemporaryManager { get; }

        public TemporaryAccessPermissionAppService(
            TemporaryAccessPermissionManager temporaryManager,
            ITemporaryAccessPermissionRepository temporaryAccessPermissionRepository)
        {
            TemporaryManager = temporaryManager;
            TemporaryAccessPermissionRepository = temporaryAccessPermissionRepository;
        }

        public virtual async Task<TemporaryAccessPermissionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TemporaryAccessPermission, TemporaryAccessPermissionDto>(
                await TemporaryAccessPermissionRepository.GetAsync(id)
            );
        }

        public virtual async Task<List<Guid>> GetAllByTemporaryUserIdAsync(Guid id)
        {
            return await TemporaryAccessPermissionRepository.GetAllByTemporaryUserIdAsync(id);
        }

        [Authorize(IdentityPermissions.TemporaryAccessPermissions.Create)]
        public async Task<TemporaryAccessPermissionDto> CreateAsync(TemporaryAccessPermissionCreateDto input)
        {
            var temporaryAccessPermission = await TemporaryManager.CreateAsync(
                input.GrantUserId,
                input.TemporaryUserId, 
                input.StartTime, 
                input.EndTime, 
                input.IsOpen,
                CurrentTenant.Id);

            return ObjectMapper.Map<TemporaryAccessPermission, TemporaryAccessPermissionDto>(
                   await TemporaryAccessPermissionRepository.InsertAsync(temporaryAccessPermission)
               );
        }

        [Authorize(IdentityPermissions.TemporaryAccessPermissions.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var temporaryAccessPermission = await TemporaryAccessPermissionRepository.GetAsync(id);
            await TemporaryAccessPermissionRepository.DeleteAsync(temporaryAccessPermission);
        }

        [Authorize(IdentityPermissions.TemporaryAccessPermissions.Update)]
        public async Task<TemporaryAccessPermissionDto> UpdateAsync(Guid id, TemporaryAccessPermissionUpdateDto input)
        {
            var temporaryAccessPermission = await TemporaryAccessPermissionRepository.GetAsync(id);
            if(input.StartTime.HasValue)
                temporaryAccessPermission.SetStartTime(input.StartTime.Value);
            if (input.EndTime.HasValue)
                temporaryAccessPermission.SetEndTime(input.EndTime.Value);
            if (input.IsOpen.HasValue)
                temporaryAccessPermission.OpenOrClose(input.IsOpen.Value);
            return ObjectMapper.Map<TemporaryAccessPermission, TemporaryAccessPermissionDto>(
                await TemporaryAccessPermissionRepository.UpdateAsync(temporaryAccessPermission)
               );
        }

        public async Task<PagedResultDto<TemporaryAccessPermissionDto>> GetListAsync(TemporaryAccessPermissionPagedListDto input)
        {
            var count = await TemporaryAccessPermissionRepository.GetCountAsync(
                input.GrantUserId, input.GrantUserName,
                input.TemporaryUserId, input.TemporaryUserName,
                input.StartTime,
                input.EndTime,
                input.IsOpen,
                input.Keyword,
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting);

            var list = await TemporaryAccessPermissionRepository.GetPagedListAsync(
                input.GrantUserId, input.GrantUserName,
                input.TemporaryUserId, input.TemporaryUserName,
                input.StartTime,
                input.EndTime,
                input.IsOpen,
                input.Keyword,
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting);
            return new PagedResultDto<TemporaryAccessPermissionDto>(count,
                ObjectMapper.Map<List<TemporaryAccessPermission>, List<TemporaryAccessPermissionDto>>(list)
            );
        }
    }
}
