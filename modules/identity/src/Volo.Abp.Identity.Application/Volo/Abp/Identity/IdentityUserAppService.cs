using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Npoi.Mapper;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Identity
{
    public class IdentityUserAppService : IdentityAppServiceBase, IIdentityUserAppService
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        protected IOrganizationUnitRepository OrganizationUnitRepository { get; }
        protected ITemporaryAccessPermissionRepository TemporaryAccessPermissionRepository { get; }
        public IdentityUserAppService(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            ITemporaryAccessPermissionRepository temporaryAccessPermissionRepository,
            IOptions<IdentityOptions> identityOptions)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            IdentityOptions = identityOptions;
            OrganizationUnitRepository = organizationUnitRepository;
            TemporaryAccessPermissionRepository = temporaryAccessPermissionRepository;

            IdentityOptions.Value.User.RequireUniqueEmail = false;

            IdentityOptions.Value.Password.RequireDigit = false;//取消需要数字限制
            IdentityOptions.Value.Password.RequireUppercase = false;//取消需要大写限制
            IdentityOptions.Value.Password.RequireLowercase = false;//取消需要小写限制
            IdentityOptions.Value.Password.RequireDigit = false;//取消数字限制
            IdentityOptions.Value.Password.RequiredUniqueChars = 0;//取消数字限制
            IdentityOptions.Value.Password.RequireNonAlphanumeric = false;//取消非字母限制
            IdentityOptions.Value.Password.RequiredLength = 6;//至少6个
        }

        //TODO: [Authorize(IdentityPermissions.Users.Default)] should go the IdentityUserAppService class.
        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<IdentityUserDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.GetByIdAsync(id)
            );
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            var count = await UserRepository.GetCountAsync(input.Filter);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<IdentityUserDto>(
                count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(list)
            );
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<PagedResultDto<IdentityUserDetailDto>> SearchListAsync(IdentityUserPagedListDto input)
        {
            var count = await UserRepository.GetPagedListCountAsync(
                input.RoleId,
                input.OrganizationUnitId,
                input.UserName,
                input.Name,
                input.Surname,
                input.Email,
                input.PhoneNumber,
                input.Keyword);
            var users = await UserRepository.GetPagedListAsync(
                input.RoleId,
                input.OrganizationUnitId,
                input.UserName,
                input.Name,
                input.Surname,
                input.Email, 
                input.PhoneNumber,
                input.Keyword,
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting);

            var list = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDetailDto>>(users);

            for (int i= 0;i < users.Count; i++)
            {
                if (list[i].Roles.IsNullOrEmpty())
                    list[i].Roles = new List<IdentityRole>();
                //添加Roles
                if (!users[i].Roles.IsNullOrEmpty())
                    foreach (var role in users[i].Roles)
                        list[i].Roles.Add(await RoleRepository.FindAsync(role.RoleId));

                if (list[i].OrganizationUnits.IsNullOrEmpty())
                    list[i].OrganizationUnits = new List<OrganizationUnit>();
                //添加Org
                if (!users[i].OrganizationUnits.IsNullOrEmpty())
                    foreach (var org in users[i].OrganizationUnits)
                        list[i].OrganizationUnits.Add(await OrganizationUnitRepository.FindAsync(org.OrganizationUnitId));
            }

            return new PagedResultDto<IdentityUserDetailDto>(count, list);
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            //TODO: Should also include roles of the related OUs.

            var roles = await UserRepository.GetRolesAsync(id);

            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles)
            );
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<List<GrantedOrganizationDto>> GetGrantedOrganizationAsync(Guid id)
        {
            var ids = await TemporaryAccessPermissionRepository.GetAllByTemporaryUserIdAsync(id);
            ids.Add(id);

            var grantedOrganizationDtoList = new List<GrantedOrganizationDto>();
            foreach (var userId in ids)
            {
                var grantedOrganizationDto = new GrantedOrganizationDto(userId);
                var ous = await UserRepository.GetOrganizationUnitsAsync(userId);
                var uous = await UserRepository.GetUserOrganizationUnitsAsync(userId);

                if (ous != null)
                {
                    List<OrganizationDto> orgs = new List<OrganizationDto>();
                    foreach (var org in ous)
                    {
                        var orgDto = new OrganizationDto();
                        orgDto.Code = org.Code;
                        orgDto.isLeader = uous.Where(o => o.OrganizationUnitId == org.Id).FirstOrDefault().IsLeader;
                        orgs.Add(orgDto);
                    }

                    grantedOrganizationDto.Orgs = orgs;
                }

                grantedOrganizationDtoList.Add(grantedOrganizationDto);
            }

            return grantedOrganizationDtoList;
        }


        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync()
        {
            var list = await RoleRepository.GetListAsync();
            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list));
        }

        [Authorize(IdentityPermissions.Users.Create)]
        public virtual async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            //await IdentityOptions.SetAsync();

            //Email Can be empty but Not BeNull
            input.Email = input.Email??string.Empty;

            var user = new IdentityUser(
                GuidGenerator.Create(),
                input.UserName,
                input.Email,
                CurrentTenant.Id
            );

            input.MapExtraPropertiesTo(user);


            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            await UpdateUserByInput(user, input);
            (await UserManager.UpdateAsync(user)).CheckErrors();


            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        [Authorize(IdentityPermissions.Users.Create)]
        public virtual async Task<IdentityUserDetailDto> VueCreateAsync(IdentityUserVueCreateDto input)
        {
            //await IdentityOptions.SetAsync();

            //Email Can be empty but Not BeNull
            input.Email = input.Email ?? string.Empty;

            var user = new IdentityUser(
                GuidGenerator.Create(),
                input.UserName,
                input.Email,
                CurrentTenant.Id
            );

            input.MapExtraPropertiesTo(user);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            await UpdateUserByInput(user, input);
            (await UserManager.UpdateAsync(user)).CheckErrors();

            //添加组织
            if (input.OrganizationUnitIds != null)
            {
                await UserManager.SetOrganizationUnitsAsync(user.Id, input.OrganizationUnitIds, input.ChargeofOrgIds);
            }

            //添加微信登录
            if (!user.PhoneNumber.IsNullOrEmpty())
                (await UserManager.AddLoginAsync(
                            user,
                            new UserLoginInfo(
                                "WeChatApp:Tel",
                                user.PhoneNumber,
                                null
                            )
                        )
                    ).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            var u =  ObjectMapper.Map<IdentityUser, IdentityUserDetailDto>(user);

            u.Roles = new List<IdentityRole>();
            //添加Roles
            foreach (var role in user.Roles)
                u.Roles.Add(await RoleRepository.FindAsync(role.RoleId));

            u.OrganizationUnits = new List<OrganizationUnit>();
            //添加Org
            foreach (var org in user.OrganizationUnits)
                u.OrganizationUnits.Add(await OrganizationUnitRepository.FindAsync(org.OrganizationUnitId));

            return u;

        }

        [Authorize(IdentityPermissions.Users.Update)]
        public virtual async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            //await IdentityOptions.SetAsync();

            //Email Can be empty but Not BeNull
            input.Email = input.Email ?? string.Empty;

            var user = await UserManager.GetByIdAsync(id);
            user.ConcurrencyStamp = input.ConcurrencyStamp;

            (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();

            await UpdateUserByInput(user, input);
            input.MapExtraPropertiesTo(user);

            (await UserManager.UpdateAsync(user)).CheckErrors();

            if (!input.Password.IsNullOrEmpty())
            {
                (await UserManager.RemovePasswordAsync(user)).CheckErrors();
                (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        [Authorize(IdentityPermissions.Users.Update)]
        public virtual async Task<IdentityUserDetailDto> VueUpdateAsync(Guid id, IdentityUserVueUpdateDto input)
        {
            //await IdentityOptions.SetAsync();

            //Email Can be empty but Not BeNull
            input.Email = input.Email ?? string.Empty;

            var user = await UserManager.GetByIdAsync(id);
            user.ConcurrencyStamp = input.ConcurrencyStamp;

            (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();

            var isSamePhoneNumber = false;
            if (!user.PhoneNumber.IsNullOrEmpty() && !input.PhoneNumber.IsNullOrEmpty() && user.PhoneNumber == input.PhoneNumber)
                isSamePhoneNumber = true;

            //如果之前有号码, 且需要改变电话号码, 先删除微信登录
            if (!user.PhoneNumber.IsNullOrEmpty() && !input.PhoneNumber.IsNullOrEmpty() && !isSamePhoneNumber)
            {
                var login = await UserManager.FindByLoginAsync("WeChatApp:Tel",
                                    user.PhoneNumber);
                if (login != null)
                    await UserManager.RemoveLoginAsync(login, "WeChatApp:Tel",
                                    user.PhoneNumber);
            }

            await UpdateUserByInput(user, input);
            input.MapExtraPropertiesTo(user);

            (await UserManager.UpdateAsync(user)).CheckErrors();

            if (!input.Password.IsNullOrEmpty())
            {
                (await UserManager.RemovePasswordAsync(user)).CheckErrors();
                (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }

            //添加组织
            if (input.OrganizationUnitIds != null)
            {
                await UserManager.SetOrganizationUnitsAsync(user.Id, input.OrganizationUnitIds, input.ChargeofOrgIds);
            }

            //添加微信登录
            if (!input.PhoneNumber.IsNullOrEmpty() && !isSamePhoneNumber)
            {
                (await UserManager.AddLoginAsync(
                                user,
                                new UserLoginInfo(
                                    "WeChatApp:Tel",
                                    user.PhoneNumber,
                                    null
                                )
                            )
                        ).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            var u = ObjectMapper.Map<IdentityUser, IdentityUserDetailDto>(user);

            u.Roles = new List<IdentityRole>();
            //添加Roles
            foreach (var role in user.Roles)
                u.Roles.Add(await RoleRepository.FindAsync(role.RoleId));

            u.OrganizationUnits = new List<OrganizationUnit>();
            //添加Org
            foreach (var org in user.OrganizationUnits)
                u.OrganizationUnits.Add(await OrganizationUnitRepository.FindAsync(org.OrganizationUnitId));

            return u;
        }

        [Authorize(IdentityPermissions.Users.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            if (CurrentUser.Id == id)
            {
                throw new BusinessException(code: IdentityErrorCodes.UserSelfDeletion);
            }

            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return;
            }

            (await UserManager.DeleteAsync(user)).CheckErrors();
        }

        [Authorize(IdentityPermissions.Users.Update)]
        public virtual async Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
        {
            var user = await UserManager.GetByIdAsync(id);
            (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            await UserRepository.UpdateAsync(user);
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<IdentityUserDto> FindByUsernameAsync(string userName)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.FindByNameAsync(userName)
            );
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<IdentityUserDto> FindByEmailAsync(string email)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.FindByEmailAsync(email)
            );
        }

        [Authorize(IdentityPermissions.Users.Update)]
        protected virtual async Task UpdateUserByInput(IdentityUser user, IdentityUserCreateOrUpdateDtoBase input)
        {
            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
            }

            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

            (await UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();

            user.Name = input.Name;
            user.Surname = input.Surname;
            (await UserManager.UpdateAsync(user)).CheckErrors();

            if (input.RoleNames != null)
            {
                (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            }
        }

        [Authorize(IdentityPermissions.Users.Create)]
        public async Task BatchCreateAsync(Guid? tenantId, List<IdentityUserExcelDto> users)
        {
            foreach (var item in users)
            {
                var user = new IdentityUser(
                    GuidGenerator.Create(),
                    item.UserName,
                    item.Email,
                    CurrentTenant.Id
                );

                user.Name = item.Name;
                user.Surname = item.Surname;

                var exist = await UserManager.FindByNameAsync(item.UserName);

                var isSamePhoneNumber = false;

                if (exist == null)
                {
                    (await UserManager.CreateAsync(user, "123456")).CheckErrors();
                }
                else
                {
                    if (!exist.PhoneNumber.IsNullOrEmpty() && !item.PhoneNumber.IsNullOrEmpty() && exist.PhoneNumber == item.PhoneNumber)
                        isSamePhoneNumber = true;

                    exist.Name = item.Name;
                    exist.Surname = item.Surname;

                    if (!string.Equals(exist.Email, item.Email, StringComparison.InvariantCultureIgnoreCase))
                    {
                        (await UserManager.SetEmailAsync(exist, item.Email)).CheckErrors();
                    }

                    //如果之前有号码, 且需要改变电话号码, 先删除微信登录
                    if (!exist.PhoneNumber.IsNullOrEmpty() && !item.PhoneNumber.IsNullOrEmpty() && !isSamePhoneNumber)
                    {
                        var login = await UserManager.FindByLoginAsync("WeChatApp:Tel",
                                            exist.PhoneNumber);
                        if (login != null)
                            await UserManager.RemoveLoginAsync(login, "WeChatApp:Tel",
                                            exist.PhoneNumber);
                    }

                    exist.SetPhoneNumber(item.PhoneNumber, false);
                    (await UserManager.UpdateAsync(exist)).CheckErrors();
                    user = exist;
                }

                user.SetPhoneNumber(item.PhoneNumber, false);

                //添加微信登录
                if (!user.PhoneNumber.IsNullOrEmpty() && !isSamePhoneNumber)
                    (await UserManager.AddLoginAsync(
                            user,
                            new UserLoginInfo(
                                "WeChatApp:Tel",
                                user.PhoneNumber,
                                null
                            )
                        )
                    ).CheckErrors();

                //添加组织
                if (item.OrganizationUnitNames != null)
                {
                    Guid[] OrganizationUnitIds = null;
                    foreach (var name in item.OrganizationUnitNames)
                    {
                        var ou = await OrganizationUnitRepository.GetAsync(name);
                        if (ou != null)
                            OrganizationUnitIds.AddLast(ou.Id);
                    }

                    Guid[] ChargeIds = null;
                    foreach (var name in item.ChargeofOrgNames)
                    {
                        var ou = await OrganizationUnitRepository.GetAsync(name);
                        if (ou != null)
                            ChargeIds.AddLast(ou.Id);
                    }
                    await UserManager.SetOrganizationUnitsAsync(user.Id, OrganizationUnitIds, ChargeIds);
                }


                //添加角色
                if (item.RoleNames != null)
                {
                    await UserManager.SetRolesAsync(user, item.RoleNames);
                }


            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        [Authorize(IdentityPermissions.Users.Export)]

        public async Task<MemoryStream> DownloadIdentityUserAsync(IdentityUserPagedListDto input)
        {
            var users = await UserRepository.GetPagedListAsync(
               input.RoleId,
               input.OrganizationUnitId,
               input.UserName,
               input.Name,
               input.Surname,
               input.Email,
               input.PhoneNumber,
               input.Keyword,
               input.SkipCount,
               input.MaxResultCount,
               input.Sorting);

            var list = new List<IdentityUserExportDto>();
            foreach (var user in users)
            {
                IdentityUserExportDto dto = new IdentityUserExportDto(ObjectMapper.Map<IdentityUser, IdentityUserDto>(user));

                //添加Roles
                if (!user.Roles.IsNullOrEmpty())
                {
                    string names = "";
                    foreach (var role in user.Roles)
                    {
                        var r = await RoleRepository.GetAsync(role.RoleId);
                        names += r.Name + ',';
                    }
                    if (!names.IsNullOrEmpty())
                        names = names.TrimEnd(',');
                    dto.RoleNames = names;//使用Split方法将字符型变量转换成字符数组
                }

                //添加Org
                if (!user.OrganizationUnits.IsNullOrEmpty())
                {
                    string names = "";

                    foreach (var org in user.OrganizationUnits)
                    {
                        var o = (await OrganizationUnitRepository.GetAsync(org.OrganizationUnitId));
                        names += o.DisplayName + ',';
                    }

                    if (!names.IsNullOrEmpty())
                        names = names.TrimEnd(',');
                    dto.OrganizationUnitNames = names;//使用Split方法将字符型变量转换成字符数组
                }


                list.Add(dto);
            }

            var mapper = new Mapper();
            MemoryStream ms = new MemoryStream();
            mapper.Save(ms, list, "sheet1", overwrite: true, xlsx: true);
            return ms;
        }
    }
}
