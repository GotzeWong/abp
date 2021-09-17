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
        public virtual Task<List<OrganizationUnitDto>> GetAllListAsync()
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
        [Route("tree/{id}")]
        public Task<List<OrganizationUnitParentDto>> GetTreeListAsync(Guid? id)
        {
            return OrganizationUnitAppService.GetTreeListAsync(id);
        }

        [HttpGet]
        [Route("search")]
        public Task<PagedResultDto<OrganizationUnitDto>> SearchListAsync(OrganizationUnitPagedListDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("batchcreate")]
        public Task BatchCreateAsync(Guid? tenantId, List<OrganizationUnitExcelDto> orgs)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("export")]
        public Task<MemoryStream> DownloadOrganizationUnitAsync(OrganizationUnitPagedListDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("leader")]
        public Task<List<IdentityUserLeadertDto>> GetAllLeadersAsync(OrganizationUnit organizationUnit)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("bind")]
        public Task BindAsync(OrganizationUnitBindDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("unbind")]
        public Task UnbindAsync(OrganizationUnitUnbindDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("set-leader")]
        public Task SetLeaderAsync(OrganizationUnitLeaderDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("menu")]
        public Task<List<OrganizationUnitMenuDto>> GetMenuListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
