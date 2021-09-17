using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Npoi.Mapper;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Identity
{
    [Authorize(IdentityPermissions.Roles.Default)]
    public class IdentityRoleAppService : IdentityAppServiceBase, IIdentityRoleAppService
    {
        protected IdentityRoleManager RoleManager { get; }
        protected IIdentityRoleRepository RoleRepository { get; }

        public IdentityRoleAppService(
            IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository)
        {
            RoleManager = roleManager;
            RoleRepository = roleRepository;
        }

        public virtual async Task<IdentityRoleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityRole, IdentityRoleDto>(
                await RoleManager.GetByIdAsync(id)
            );
        }

        public virtual async Task<ListResultDto<IdentityRoleDto>> GetAllListAsync()
        {
            var list = await RoleRepository.GetListAsync();
            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list)
            );
        }

        public virtual async Task<PagedResultDto<IdentityRoleDto>> GetListAsync(GetIdentityRolesInput input)
        {
            var list = await RoleRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            var totalCount = await RoleRepository.GetCountAsync(input.Filter);

            return new PagedResultDto<IdentityRoleDto>(
                totalCount,
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list)
                );
        }


        public virtual async Task<PagedResultDto<IdentityRoleDto>> SearchListAsync(IdentityRolePagedListDto input)
        {
            var id = CurrentTenant.Id;
            var count = await RoleRepository.SearchListCountAsync(input.Name, input.Keyword);
            var list = await RoleRepository.SearchListAsync(input.Name, input.Sorting, input.MaxResultCount, input.SkipCount, input.Keyword);

            return new PagedResultDto<IdentityRoleDto>(
                count,
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list)
                );
        }

        [Authorize(IdentityPermissions.Roles.Create)]
        public virtual async Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            var role = new IdentityRole(
                GuidGenerator.Create(),
                input.Name,
                CurrentTenant.Id
            )
            {
                IsDefault = input.IsDefault,
                IsPublic = input.IsPublic
            };

            input.MapExtraPropertiesTo(role);

            (await RoleManager.CreateAsync(role)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityRole, IdentityRoleDto>(role);
        }

        [Authorize(IdentityPermissions.Roles.Update)]
        public virtual async Task<IdentityRoleDto> UpdateAsync(Guid id, IdentityRoleUpdateDto input)
        {
            var role = await RoleManager.GetByIdAsync(id);
            role.ConcurrencyStamp = input.ConcurrencyStamp;

            (await RoleManager.SetRoleNameAsync(role, input.Name)).CheckErrors();

            role.IsDefault = input.IsDefault;
            role.IsPublic = input.IsPublic;

            input.MapExtraPropertiesTo(role);

            (await RoleManager.UpdateAsync(role)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityRole, IdentityRoleDto>(role);
        }

        [Authorize(IdentityPermissions.Roles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var role = await RoleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return;
            }

            (await RoleManager.DeleteAsync(role)).CheckErrors();
        }


        [Authorize(IdentityPermissions.Roles.Create)]
        public async Task BatchCreateAsync(Guid? tenantId, List<IdentityRoleExcelDto> roles)
        {
            foreach (var item in roles)
            {
                var role = new IdentityRole(
                    GuidGenerator.Create(),
                    item.Name,
                    CurrentTenant.Id
                )
                {
                };

                if (RoleManager.FindByNameAsync(role.Name) != null)
                    (await RoleManager.UpdateAsync(role)).CheckErrors();
                else
                    (await RoleManager.CreateAsync(role)).CheckErrors();

            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        [Authorize(IdentityPermissions.Roles.Export)]
        public async Task<MemoryStream> DownloadIdentityRoleAsync(IdentityRolePagedListDto input)
        {
            var roles = await RoleRepository.SearchListAsync(input.Name, input.Sorting, input.MaxResultCount, input.SkipCount, input.Keyword);

            var list = new List<IdentityRoleExcelDto>();
            foreach (var role in roles)
            {
                IdentityRoleExcelDto dto = new IdentityRoleExcelDto(ObjectMapper.Map<IdentityRole, IdentityRoleDto>(role));
                list.Add(dto);
            }

            var mapper = new Mapper();
            MemoryStream ms = new MemoryStream();
            mapper.Save(ms, list, "sheet1", overwrite: true, xlsx: true);
            return ms;
        }

    }
}
