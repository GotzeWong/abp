using Microsoft.AspNetCore.Authorization;
using Npoi.Mapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectMapping;

namespace Volo.Abp.Identity
{
    [Authorize(IdentityPermissions.Organizations.Default)]
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        protected OrganizationUnitManager OrganizationManager { get; }
        protected IOrganizationUnitRepository OrganizationRepository { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IdentityUserManager UserManager { get; }

        public OrganizationUnitAppService(
            IdentityUserManager userManager,
           IIdentityUserRepository userRepository,
           OrganizationUnitManager organizationManager,
           IOrganizationUnitRepository organizationRepository)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            OrganizationManager = organizationManager;
            OrganizationRepository = organizationRepository;
        }

        public virtual async Task<List<OrganizationUnitDto>> GetAllListAsync()
        {
            return ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(await OrganizationRepository.GetListAsync());
        }
        public virtual async Task<List<IdentityUserLeadertDto>> GetAllLeadersAsync(OrganizationUnit ou)
        {
            var leaders = await OrganizationRepository.GetAllLeadersAsync(ou);
            return ObjectMapper.Map<List<IdentityUser>, List<IdentityUserLeadertDto>>(leaders);
        }

        public virtual async Task<List<OrganizationUnitMenuDto>> GetMenuListAsync()
        {
            var list = new List<OrganizationUnitMenuDto>();

            OrganizationUnitMenuDto organizationUnitMenuDto = new OrganizationUnitMenuDto("销售区域", "hierarchy");

            var existedSaleOrg = await OrganizationRepository.GetSaleOrgAsync();

            if(null == existedSaleOrg)
                throw new UserFriendlyException($"获取失败！请通知管理员设置销售部门。", "AgentHub.Organization:00002");
            else
                organizationUnitMenuDto.submenu = await GetMenuListAsync(existedSaleOrg.Id);

            list.Add(organizationUnitMenuDto);
            return list;
        }

        public async Task<List<OrganizationUnitSubMenuDto>> GetMenuListAsync(Guid? parentId)
        {
            var list = await OrganizationRepository.GetChildrenAsync(parentId);

            if (list != null)
            {
                var mlist = new List<OrganizationUnitSubMenuDto>();
                foreach (var ou in list)
                {
                    var sub = new OrganizationUnitSubMenuDto();
                    sub.name = ou.DisplayName;
                    sub.value = ou.Code;
                    sub.submenu = await GetMenuListAsync(ou.Id);

                    mlist.Add(sub);
                }
                return mlist;
            } else
                return null;
            
        }


        public virtual async Task<List<OrganizationUnitParentDto>> GetTreeListAsync(Guid? parentId)
        {
            List<OrganizationUnit> list = new List<OrganizationUnit>();

            list = await OrganizationRepository.GetChildrenAsync(parentId);

            var ouVueList = ObjectMapper.Map<List<OrganizationUnit> ,List<OrganizationUnitParentDto>>(list);

            foreach (var ou in ouVueList)
            {
                ou.Children = await GetTreeListAsync(ou.Id);
                ou.Leaders = await GetAllLeadersAsync(list.Where(o => o.Id == ou.Id).FirstOrDefault());
                ou.Total = await UserRepository.GetUsersCountInOrganizationUnitAsync(ou.Id);
                foreach (var child in ou.Children)
                    ou.AllMembers += child.AllMembers;
                ou.AllMembers += ou.Total;
            }
            
            return ouVueList;
        }

        public virtual async Task<PagedResultDto<OrganizationUnitDto>> SearchListAsync(OrganizationUnitPagedListDto input)
        {
            var count = await OrganizationRepository.SearchListCountAsync(
                input.Code,
                input.DisplayName,
                input.Status,
                input.Remark,
                input.Keyword);
            var orgs = await OrganizationRepository.SearchListAsync(
                input.Code,
                input.DisplayName,
                input.Status,
                input.Remark,
                input.Keyword,
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting);

            return new PagedResultDto<OrganizationUnitDto>(count, ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(orgs));
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

            if (input.isSale.HasValue && input.isSale.Value)
            {
                var existedSaleOrg = await OrganizationRepository.GetSaleOrgAsync();
                if (null != existedSaleOrg)
                    throw new UserFriendlyException($"设置失败！已设置{existedSaleOrg.DisplayName}为销售部门，不能重复设置。", "AgentHub.Organization:00001");
                else
                    ou.SetSale(true);
            }

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

            if (input.isSale.HasValue)//如果设置
                if (input.isSale.Value)
                {
                    var existedSaleOrg = await OrganizationRepository.GetSaleOrgAsync();
                    if (null != existedSaleOrg && existedSaleOrg.Id != id)
                        throw new UserFriendlyException($"设置失败！已设置{existedSaleOrg.DisplayName}为销售部门，不能重复设置。", "AgentHub.Organization:00001");
                    else
                        ou.SetSale(true);
                }
                else
                {
                    ou.SetSale(false);
                }

            await OrganizationManager.UpdateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }


        [Authorize(IdentityPermissions.Organizations.OUManagement)]
        public virtual async Task BindAsync(OrganizationUnitBindDto input)
        {
            if (input.OrganizationUnitId != null)
            {
                foreach(var userId in input.UserIds)
                    await UserManager.SetOrganizationUnitsAsync(userId, input.OrganizationUnitId, input.OrgLeaderIds);
            }
        }

        [Authorize(IdentityPermissions.Organizations.OUManagement)]
        public virtual async Task UnbindAsync(OrganizationUnitUnbindDto input)
        {
            if (input.OrganizationUnitId != null)
            {
                foreach (var userId in input.UserIds)
                    await UserManager.RemoveFromOrganizationUnitAsync(userId, input.OrganizationUnitId);
            }
        }

        [Authorize(IdentityPermissions.Organizations.Create)]
        public async Task BatchCreateAsync(Guid? tenantId, List<OrganizationUnitExcelDto> orgs)
        {
            foreach (var item in orgs)
            {
                OrganizationUnit parent = null;
                if (!String.IsNullOrEmpty(item.ParentName))//如果需要绑定父节点
                {
                    parent = await OrganizationManager.GetByNameAsync(item.ParentName);
                    if (parent == null)//该父节点没有创建
                    {
                        var parentOu = new OrganizationUnit(
                           GuidGenerator.Create(),
                           item.ParentName,
                           null,
                           CurrentTenant.Id
                       );
                        await OrganizationManager.CreateAsync(parentOu);
                        parent = parentOu;
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }

                var ou = new OrganizationUnit(
                   GuidGenerator.Create(),
                   item.DisplayName,
                   parent != null ? parent.Id : null,
                   CurrentTenant.Id
               );

                if (item.IsSale)//如果是销售部门
                    ou.SetSale(true);

                if (await OrganizationManager.GetByNameAsync(item.DisplayName) != null)
                    await OrganizationManager.UpdateAsync(ou);
                else
                    await OrganizationManager.CreateAsync(ou);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        [Authorize(IdentityPermissions.Organizations.Export)]
        public async Task<MemoryStream> DownloadOrganizationUnitAsync(OrganizationUnitPagedListDto input)
        {
            var orgs = await OrganizationRepository.SearchListAsync(
                            input.Code,
                            input.DisplayName,
                            input.Status,
                            input.Remark,
                            input.Keyword,
                            input.SkipCount,
                            input.MaxResultCount,
                            input.Sorting);

            var list = new List<OrganizationUnitExportDto>();
            foreach (var org in orgs)
            {
                OrganizationUnitExportDto dto = new OrganizationUnitExportDto(ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(org));
                if (dto.ParentName != null)
                    dto.ParentName = (await OrganizationRepository.GetByCodeAsync(dto.ParentName)).DisplayName;

                var leaders = await OrganizationRepository.GetAllLeadersAsync(org);

                string leaderNames = "";
                foreach (var leader in leaders)
                {
                    if (!leader.Name.IsNullOrEmpty())
                        leaderNames += leader.Name + ',';
                }

                if (!leaderNames.IsNullOrEmpty())
                    leaderNames = leaderNames.TrimEnd(',');
                dto.LeaderNames = leaderNames;

                var users = await UserRepository.GetPagedListAsync(null, org.Id);

                string names = "";
                foreach (var user in users)
                {
                    if (!user.Name.IsNullOrEmpty())
                        names += user.Name + ',';
                }

                if (!names.IsNullOrEmpty())
                    names = names.TrimEnd(',');
                dto.UserNames = names;//使用Split方法将字符型变量转换成字符数组

                if (dto.Status == "Enable")
                    dto.Status = "开启";
                if (dto.Status == "Disable")
                    dto.Status = "关闭";
                if (dto.Status == "Null")
                    dto.Status = "";

                list.Add(dto);
            }

            var mapper = new Mapper();
            MemoryStream ms = new MemoryStream();
            mapper.Save(ms, list, "sheet1", overwrite: true, xlsx: true);
            return ms;
        }

        public async Task SetLeaderAsync(OrganizationUnitLeaderDto input)
        {
            await UserManager.SetOrganizationUnitsAsync(input.UserId, input.OrgId, input.isLeader);
        }

    }
}
