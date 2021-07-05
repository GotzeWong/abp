using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("organization")]
    [ControllerName("Organization")]
    [Route("api/identity/organizations")]
    public class OrganizationUnitController : AbpController, IOrganizationUnitAppService
    {
        protected IOrganizationUnitAppService OrganizationUnitAppService { get; }

        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            OrganizationUnitAppService = organizationUnitAppService;
        }

        [HttpGet]
        [Route("all")]
        public virtual Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
        {
            return OrganizationUnitAppService.GetAllListAsync();
        }

        [HttpGet]
        public virtual Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitsInput input)
        {
            return OrganizationUnitAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return OrganizationUnitAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            return OrganizationUnitAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            return OrganizationUnitAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return OrganizationUnitAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("list")]
        public Task<List<OrganizationUnitParentDto>> GetArrangedListAsync()
        {
            return OrganizationUnitAppService.GetArrangedListAsync();
        }
    }
}
