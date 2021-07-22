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
    [Area("organization")]
    [ControllerName("VueOrganization")]
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
        public virtual async Task<VueTResultDto<ListResultDto<OrganizationUnitDto>>> GetAllListAsync()
        {
            return new VueTResultDto<ListResultDto<OrganizationUnitDto>>(await OrganizationUnitAppService.GetAllListAsync());
        }

        [HttpGet]
        public virtual async Task<VueTResultDto<PagedResultDto<OrganizationUnitDto>>> GetListAsync(GetOrganizationUnitsInput input)
        {
            return new VueTResultDto<PagedResultDto<OrganizationUnitDto>>(await OrganizationUnitAppService.GetListAsync(input));
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
        [Route("list")]
        public async Task<VueTResultDto<List<OrganizationUnitParentDto>>> GetArrangedListAsync()
        {
            return new VueTResultDto<List<OrganizationUnitParentDto>>(await OrganizationUnitAppService.GetArrangedListAsync());
        }
    }

}
