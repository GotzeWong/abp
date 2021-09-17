using JetBrains.Annotations;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class TemporaryAccessPermissionUpdateDto
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsOpen { get; set; }

        public TemporaryAccessPermissionUpdateDto() { }

    }
}
