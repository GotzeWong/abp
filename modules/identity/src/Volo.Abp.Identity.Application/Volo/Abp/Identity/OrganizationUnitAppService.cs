using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectMapping;

namespace Volo.Abp.Identity
{
    [Authorize(IdentityPermissions.Organizations.Default)]
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        protected OrganizationUnitManager OrganizationManager { get; }
        protected IOrganizationUnitRepository OrganizationRepository { get; }

        public OrganizationUnitAppService(
           OrganizationUnitManager organizationManager,
           IOrganizationUnitRepository organizationRepository)
        {
            OrganizationManager = organizationManager;
            OrganizationRepository = organizationRepository;
        }


        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
        {
            return ObjectMapper.Map<List<OrganizationUnit>, ListResultDto<OrganizationUnitDto>>(await OrganizationRepository.GetListAsync());
        }

        public virtual async Task<List<OrganizationUnitParentDto>> GetArrangedListAsync()
        {
            //throw new NotImplementedException();
            var list = await OrganizationRepository.GetChildrenAsync(null);

            var ouVueList = new List<OrganizationUnitParentDto>();

            foreach (OrganizationUnit oud in list)
            {
                OrganizationUnitParentDto ouVue = ObjectMapper.Map<OrganizationUnit, OrganizationUnitParentDto>(oud);

                ouVue.Children = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitChildDto>>(await OrganizationRepository.GetAllChildrenWithParentCodeAsync(oud.Code, oud.Id));

                ouVueList.Add(ouVue);
            }

            //return await Task.FromResult(ouVueList);
            return new List<OrganizationUnitParentDto>(ouVueList);
        }

        public virtual async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(
                await OrganizationManager.GetByIdAsync(id)
            );
        }

        public Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitsInput input)
        {
            throw new NotImplementedException();
        }

        [Authorize(IdentityPermissions.Organizations.Create)]
        public virtual async Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var ou = new OrganizationUnit(
                GuidGenerator.Create(),
                input.DisplayName, 
                input.OrderNo, 
                input.Remark,
                input.ParentId,
                CurrentTenant.Id
            );

            input.MapExtraPropertiesTo(ou);

            await OrganizationManager.CreateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        [Authorize(IdentityPermissions.Organizations.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var ou = await OrganizationManager.GetByIdAsync(id);
            if (ou == null)
            {
                return;
            }

            await OrganizationManager.DeleteAsync(id);
        }

        [Authorize(IdentityPermissions.Organizations.Update)]
        public virtual async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var ou = await OrganizationManager.GetByIdAsync(id);
            ou.ConcurrencyStamp = input.ConcurrencyStamp;
            ou.DisplayName = input.DisplayName;
            ou.Remark = input.Remark;
            ou.Status = input.Status;
            ou.OrderNo = input.OrderNo;
            
            input.MapExtraPropertiesTo(ou);

            await OrganizationManager.UpdateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

    }
}
