using AgentHub.Shared;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class IdentityUserPagedListDto : PagedSortedAndFilteredResultRequestDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 花名
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        public Guid? OrganizationUnitId { get; set; }
        public Guid? RoleId { get; set; }
        
            
    }
}
