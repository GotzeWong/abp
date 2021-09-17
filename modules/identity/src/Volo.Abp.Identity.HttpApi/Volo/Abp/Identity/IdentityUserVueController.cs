using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgentHub.Shared;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    /// <summary>
    /// 身份管理模块
    /// </summary>
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("VueUser")]
    [Route("vue/api/identity/users")]
    public class IdentityUserVueController : AbpController
    {
        protected IIdentityUserAppService UserAppService { get; }

        public IdentityUserVueController(IIdentityUserAppService userAppService)
        {
            UserAppService = userAppService;
        }


        [HttpGet]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<IdentityUserDto>> GetAsync(Guid id)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.GetAsync(id));
        }

        //[HttpGet]
        //public virtual async Task<VueTResultDto<PagedResultDto<IdentityUserDto>>> GetListAsync(GetIdentityUsersInput input)
        //{
        //    return new VueTResultDto<PagedResultDto<IdentityUserDto>>(await UserAppService.GetListAsync(input));
        //}

        [HttpGet]
        public async Task<VueTResultDto<PagedResultDto<IdentityUserDetailDto>>> GetListAsync(IdentityUserPagedListDto input)
        {
            return new VueTResultDto<PagedResultDto<IdentityUserDetailDto>>(await UserAppService.SearchListAsync(input));
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <remarks>
        /// <para>{</para>
        /// <para>tenantId：Guid，nullable</para>
        /// <para>userName：string，required</para>
        /// <para>password：string，required</para>
        /// <para>name：string，nullable</para>
        /// <para>surname：string，nullable</para>
        /// <para>phoneNumber：int，nullable</para>
        /// <para>lockoutEnabled：bool</para>
        /// <para>roleNames：string，string[]</para>
        /// <para>}</para>
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<VueTResultDto<IdentityUserDetailDto>> CreateAsync(IdentityUserVueCreateDto input)
        {
            return new VueTResultDto<IdentityUserDetailDto>(await UserAppService.VueCreateAsync(input));
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<VueTResultDto<IdentityUserDetailDto>> UpdateAsync(Guid id, IdentityUserVueUpdateDto input)
        {
            return new VueTResultDto<IdentityUserDetailDto>(await UserAppService.VueUpdateAsync(id, input));
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<VueResultDto> DeleteAsync(Guid id)
        {
            await UserAppService.DeleteAsync(id);
            return new VueTResultDto<bool>(true);
        }

        [HttpGet]
        [Route("{id}/roles")]
        public virtual async Task<VueTResultDto<ListResultDto<IdentityRoleDto>>> GetRolesAsync(Guid id)
        {
            return new VueTResultDto<ListResultDto<IdentityRoleDto>>(await UserAppService.GetRolesAsync(id));
        }

        [HttpGet]
        [Route("assignable-roles")]
        public virtual async Task<VueTResultDto<ListResultDto<IdentityRoleDto>>> GetAssignableRolesAsync()
        {
            return new VueTResultDto<ListResultDto<IdentityRoleDto>>(await UserAppService.GetAssignableRolesAsync());
        }

        [HttpPut]
        [Route("{id}/roles")]
        public virtual async Task<VueResultDto> UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
        {
            await UserAppService.UpdateRolesAsync(id, input);
            return new VueTResultDto<bool>(true);
        }

        [HttpGet]
        [Route("by-username/{userName}")]
        public virtual async Task<VueTResultDto<IdentityUserDto>> FindByUsernameAsync(string userName)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.FindByUsernameAsync(userName));
        }

        [HttpGet]
        [Route("by-email/{email}")]
        public virtual async Task<VueTResultDto<IdentityUserDto>> FindByEmailAsync(string email)
        {
            return new VueTResultDto<IdentityUserDto>(await UserAppService.FindByEmailAsync(email));
        }

        [HttpPost]
        [Route("batch-create")]
        public async Task<VueResultDto> BatchCreateAsync(Guid? tenantId, List<IdentityUserExcelDto> users)
        {
            await UserAppService.BatchCreateAsync(tenantId, users);
            return new VueTResultDto<bool>(true);
        }

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> DownloadIdentityUserAsync(IdentityUserPagedListDto input)
        {
            var stream = await UserAppService.DownloadIdentityUserAsync(input);
            return File(stream.ToArray(), HttpFileType.Excel, "用户信息表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
        }
    }
}
