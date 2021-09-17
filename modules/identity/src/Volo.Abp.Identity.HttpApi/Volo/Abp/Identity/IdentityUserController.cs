using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("User")]
    [Route("api/identity/users")]
    public class IdentityUserController : AbpController, IIdentityUserAppService
    {
        protected IIdentityUserAppService UserAppService { get; }

        public IdentityUserController(IIdentityUserAppService userAppService)
        {
            UserAppService = userAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<IdentityUserDto> GetAsync(Guid id)
        {
            return UserAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            return UserAppService.GetListAsync(input);
        }

        [HttpPost]
        public virtual Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            return UserAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            return UserAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return UserAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}/roles")]
        public virtual Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            return UserAppService.GetRolesAsync(id);
        }

        [HttpGet]
        [Route("assignable-roles")]
        public Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync()
        {
            return UserAppService.GetAssignableRolesAsync();
        }

        [HttpPut]
        [Route("{id}/roles")]
        public virtual Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
        {
            return UserAppService.UpdateRolesAsync(id, input);
        }

        [HttpGet]
        [Route("by-username/{userName}")]
        public virtual Task<IdentityUserDto> FindByUsernameAsync(string userName)
        {
            return UserAppService.FindByUsernameAsync(userName);
        }

        [HttpGet]
        [Route("by-email/{email}")]
        public virtual Task<IdentityUserDto> FindByEmailAsync(string email)
        {
            return UserAppService.FindByEmailAsync(email);
        }

        [HttpGet]
        [Route("search")]
        public async Task<PagedResultDto<IdentityUserDetailDto>> SearchListAsync(IdentityUserPagedListDto input)
        {
            return  await UserAppService.SearchListAsync(input);

        }

        [HttpPost]
        [Route("batchcreate")]
        public Task BatchCreateAsync(Guid? tenantId, List<IdentityUserExcelDto> users)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("vuecreate")]
        public Task<IdentityUserDetailDto> VueCreateAsync(IdentityUserVueCreateDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("vueupdate/{id}")]
        public Task<IdentityUserDetailDto> VueUpdateAsync(Guid id, IdentityUserVueUpdateDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("export")]
        public Task<MemoryStream> DownloadIdentityUserAsync(IdentityUserPagedListDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}/granted-organizations")]
        public async Task<List<GrantedOrganizationDto>> GetGrantedOrganizationAsync(Guid id)
        {
            return await UserAppService.GetGrantedOrganizationAsync(id);
        }
    }
}
