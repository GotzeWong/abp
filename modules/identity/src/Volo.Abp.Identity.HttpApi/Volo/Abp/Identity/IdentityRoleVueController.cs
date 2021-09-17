using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AgentHub.Shared;
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

        //[HttpGet]
        //public virtual async Task<VueTResultDto<PagedResultDto<IdentityRoleDto>>> GetListAsync(GetIdentityRolesInput input)
        //{
        //    return new VueTResultDto<PagedResultDto<IdentityRoleDto>>(await RoleAppService.GetListAsync(input));
        //}

        [HttpGet]
        public virtual async Task<VueTResultDto<PagedResultDto<IdentityRoleDto>>> GetListAsync(IdentityRolePagedListDto input)
        {
            return new VueTResultDto<PagedResultDto<IdentityRoleDto>>(await RoleAppService.SearchListAsync(input));
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
        public virtual async Task<VueTResultDto<IdentityRoleDto>> UpdateAsync(Guid id, IdentityRoleUpdateDto input)
        {
            return new VueTResultDto<IdentityRoleDto>(await RoleAppService.UpdateAsync(id, input));
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<bool>> DeleteAsync(Guid id)
        {
            await RoleAppService.DeleteAsync(id);
            return new VueTResultDto<bool>(true);
        }

        [HttpPost]
        [Route("batch-create")]
        public async Task<VueResultDto> BatchCreateAsync(Guid? tenantId, List<IdentityRoleExcelDto> roles)
        {
            await RoleAppService.BatchCreateAsync(tenantId, roles);
            return new VueTResultDto<bool>(true);
        }

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> DownloadIdentityRoleAsync(IdentityRolePagedListDto input)
        {
            var stream = await RoleAppService.DownloadIdentityRoleAsync(input);
            return File(stream.ToArray(), HttpFileType.Excel, "用户信息表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
        }
    }
}
