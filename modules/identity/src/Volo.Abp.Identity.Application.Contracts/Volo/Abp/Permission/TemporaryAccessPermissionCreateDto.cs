using JetBrains.Annotations;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class TemporaryAccessPermissionCreateDto
    {
        public Guid GrantUserId { get; set; }
        public Guid TemporaryUserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsOpen { get; set; }

        public TemporaryAccessPermissionCreateDto() { }

    }
}
