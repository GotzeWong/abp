using AgentHub.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity
{
    public class TemporaryAccessPermissionPagedListDto : PagedSortedAndFilteredResultRequestDto
    {
        //授权人
        public Guid? GrantUserId { get; set; }
        //授权人姓名
        public string GrantUserName { get; set; }
        //被授权人
        public Guid? TemporaryUserId { get; set; }
        //被授权人姓名
        public string TemporaryUserName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsOpen { get; set; }
    }
}
