using System;
using System.Threading.Tasks;
using AgentHubLibrary;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("VueUser")]
    [Route("vue/api/identity/users")]
    public class IdentityUserVueController : AbpController
    {
        protected IIdentityUserAppService UserAppService { get; }

        public IdentityUserVueController(IIdentityUserAppService userAppService)
        {
            UserAppService = userAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<IdentityUserDto>> GetAsync(Guid id)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.GetAsync(id));
        }

        [HttpGet]
        public virtual async Task<VueTResultDto<PagedResultDto<IdentityUserDto>>> GetListAsync(GetIdentityUsersInput input)
        {
            return new VueTResultDto<PagedResultDto<IdentityUserDto>>(await UserAppService.GetListAsync(input));
        }

        [HttpPost]
        public virtual async Task<VueTResultDto<IdentityUserDto>> CreateAsync(IdentityUserCreateDto input)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.CreateAsync(input));
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<IdentityUserDto>> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.UpdateAsync(id, input));
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<VueResultDto> DeleteAsync(Guid id)
        {
            await UserAppService.DeleteAsync(id);
            return new VueTResultDto<bool>(true);
        }

        [HttpGet]
        [Route("{id}/roles")]
        public virtual async Task<VueTResultDto<ListResultDto<IdentityRoleDto>>> GetRolesAsync(Guid id)
        {
            return new VueTResultDto<ListResultDto<IdentityRoleDto>>(await UserAppService.GetRolesAsync(id));
        }

        [HttpGet]
        [Route("assignable-roles")]
        public virtual async Task<VueTResultDto<ListResultDto<IdentityRoleDto>>> GetAssignableRolesAsync()
        {
            return new VueTResultDto<ListResultDto<IdentityRoleDto>>(await UserAppService.GetAssignableRolesAsync());
        }

        [HttpPut]
        [Route("{id}/roles")]
        public virtual Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
        {
            return UserAppService.UpdateRolesAsync(id, input);
        }

        [HttpGet]
        [Route("by-username/{userName}")]
        public virtual async Task<VueTResultDto<IdentityUserDto>> FindByUsernameAsync(string userName)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.FindByUsernameAsync(userName));
        }

        [HttpGet]
        [Route("by-email/{email}")]
        public virtual async Task<VueTResultDto<IdentityUserDto>> FindByEmailAsync(string email)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.FindByEmailAsync(email));
        }
    }
}
