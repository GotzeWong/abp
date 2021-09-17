using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgentHub.Shared;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("TemporaryAccessPermission")]
    [Route("/vue/api/identity/temporary")]
    public class TemporaryAccessPermissionVueController : AbpController
    {
        protected ITemporaryAccessPermissionAppService TemporaryAccessPermissionAppService { get; }

        public TemporaryAccessPermissionVueController(ITemporaryAccessPermissionAppService temporaryAccessPermissionAppService)
        {
            TemporaryAccessPermissionAppService = temporaryAccessPermissionAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<TemporaryAccessPermissionDto>> GetAsync(Guid id)
        {
            return new VueTResultDto<TemporaryAccessPermissionDto>(await TemporaryAccessPermissionAppService.GetAsync(id));
        }

        [HttpGet]
        [Route("granted/{id}")]
        public virtual async Task<VueTResultDto<List<Guid>>> GetAllByTemporaryUserIdAsync(Guid id)
        {
            return new VueTResultDto<List<Guid>>(await TemporaryAccessPermissionAppService.GetAllByTemporaryUserIdAsync(id));
        }

        [HttpGet]
        public async Task<VueTResultDto<PagedResultDto<TemporaryAccessPermissionDto>>> GetListAsync(TemporaryAccessPermissionPagedListDto input)
        {
            return new VueTResultDto<PagedResultDto<TemporaryAccessPermissionDto>>(await TemporaryAccessPermissionAppService.GetListAsync(input));
        }

        [HttpPost]
        public virtual async Task<VueTResultDto<TemporaryAccessPermissionDto>> CreateAsync(TemporaryAccessPermissionCreateDto input)
        {
            return new VueTResultDto<TemporaryAccessPermissionDto>(await TemporaryAccessPermissionAppService.CreateAsync(input));
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<TemporaryAccessPermissionDto>> UpdateAsync(Guid id, TemporaryAccessPermissionUpdateDto input)
        {
            return new VueTResultDto<TemporaryAccessPermissionDto>(await TemporaryAccessPermissionAppService.UpdateAsync(id, input));
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<VueResultDto> DeleteAsync(Guid id)
        {
            await TemporaryAccessPermissionAppService.DeleteAsync(id);
            return new VueTResultDto<bool>(true);
        }

    }
}
