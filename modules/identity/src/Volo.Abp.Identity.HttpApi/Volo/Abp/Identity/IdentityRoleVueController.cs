using System;
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
    [ControllerName("VueRole")]
    [Route("vue/api/identity/roles")]
    public class IdentityRoleVueController : AbpController
    {
        protected IIdentityRoleAppService RoleAppService { get; }

        public IdentityRoleVueController(IIdentityRoleAppService roleAppService)
        {
            RoleAppService = roleAppService;
        }

        [HttpGet]
        [Route("all")]
        public virtual async Task<VueTResultDto<ListResultDto<IdentityRoleDto>>> GetAllListAsync()
        {
            return new VueTResultDto<ListResultDto<IdentityRoleDto>>( await RoleAppService.GetAllListAsync());
        }

        [HttpGet]
        public virtual async Task<VueTResultDto<PagedResultDto<IdentityRoleDto>>> GetListAsync(GetIdentityRolesInput input)
        {
            return new VueTResultDto<PagedResultDto<IdentityRoleDto>>(await RoleAppService.GetListAsync(input));
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<IdentityRoleDto>> GetAsync(Guid id)
        {
            return new VueTResultDto<IdentityRoleDto>(await RoleAppService.GetAsync(id));
        }

        [HttpPost]
        public virtual async Task<VueTResultDto<IdentityRoleDto>> CreateAsync(IdentityRoleCreateDto input)
        {
            return new VueTResultDto<IdentityRoleDto>(await RoleAppService.CreateAsync(input));
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<bool>> UpdateAsync(Guid id, IdentityRoleUpdateDto input)
        {
            await RoleAppService.UpdateAsync(id, input);
            return new VueTResultDto<bool>(true);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<bool>> DeleteAsync(Guid id)
        {
            await RoleAppService.DeleteAsync(id);
            return new VueTResultDto<bool>(true);
        }
    }
}
