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
    [Area("organizationunit")]
    [ControllerName("VueOrganizationUnit")]
    [Route("vue/api/identity/organizations")]
    public class OrganizationUnitVueController : AbpController
    {
        protected IOrganizationUnitAppService OrganizationUnitAppService { get; }

        public OrganizationUnitVueController(IOrganizationUnitAppService organizationUnitAppService)
        {
            OrganizationUnitAppService = organizationUnitAppService;
        }

        [HttpGet]
        [Route("all")]
        public virtual async Task<VueTResultDto<List<OrganizationUnitDto>>> GetAllListAsync()
        {
            return new VueTResultDto<List<OrganizationUnitDto>>(await OrganizationUnitAppService.GetAllListAsync());
        }

        [HttpGet]
        public virtual async Task<VueTResultDto<PagedResultDto<OrganizationUnitDto>>> GetListAsync(OrganizationUnitPagedListDto input)
        {
            return new VueTResultDto<PagedResultDto<OrganizationUnitDto>>(await OrganizationUnitAppService.SearchListAsync(input));
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<OrganizationUnitDto>> GetAsync(Guid id)
        {
            return new VueTResultDto<OrganizationUnitDto>(await OrganizationUnitAppService.GetAsync(id));
        }

        [HttpPost]
        public virtual async Task<VueTResultDto<OrganizationUnitDto>> CreateAsync(OrganizationUnitCreateDto input)
        {
            return new VueTResultDto<OrganizationUnitDto>(await OrganizationUnitAppService.CreateAsync(input));
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<OrganizationUnitDto>> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            return new VueTResultDto<OrganizationUnitDto>(await OrganizationUnitAppService.UpdateAsync(id, input));
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<bool>> DeleteAsync(Guid id)
        {
            await OrganizationUnitAppService.DeleteAsync(id);
            return new VueTResultDto<bool>(true);
        }

        [HttpGet]
        [Route("tree/{id?}")]
        public async Task<VueTResultDto<List<OrganizationUnitParentDto>>> GetTreeListAsync(Guid? id)
        {
            return new VueTResultDto<List<OrganizationUnitParentDto>>(await OrganizationUnitAppService.GetTreeListAsync(id));
        }

        [HttpGet]
        [Route("menu")]
        public async Task<VueTResultDto<List<OrganizationUnitMenuDto>>> GetMenuListAsync()
        {
            return new VueTResultDto<List<OrganizationUnitMenuDto>>(await OrganizationUnitAppService.GetMenuListAsync());
        }

        [HttpPost]
        [Route("batch-create")]
        public async Task<VueResultDto> BatchCreateAsync(Guid? tenantId, List<OrganizationUnitExcelDto> orgs)
        {
            await OrganizationUnitAppService.BatchCreateAsync(tenantId, orgs);
            return new VueTResultDto<bool>(true);
        }

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> DownloadOrganizationUnitAsync(OrganizationUnitPagedListDto input)
        {
            var stream = await OrganizationUnitAppService.DownloadOrganizationUnitAsync(input);
            return File(stream.ToArray(), HttpFileType.Excel, "部门信息表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
        }

        [HttpPost]
        [Route("bind")]
        public async Task<VueResultDto> BindAsync(OrganizationUnitBindDto input)
        {
            await OrganizationUnitAppService.BindAsync(input);
            return new VueTResultDto<bool>(true);
        }

        [HttpPost]
        [Route("unbind")]
        public async Task<VueResultDto> UnbindAsync(OrganizationUnitUnbindDto input)
        {
            await OrganizationUnitAppService.UnbindAsync(input);
            return new VueTResultDto<bool>(true);
        }

        [HttpPost]
        [Route("set-leader")]
        public async Task<VueResultDto> SetLeaderAsync(OrganizationUnitLeaderDto input)
        {
            await OrganizationUnitAppService.SetLeaderAsync(input);
            return new VueTResultDto<bool>(true);
        }
    }

}
