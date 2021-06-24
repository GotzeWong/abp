using System;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.TenantManagement
{
    public class TenantDto : ExtensibleEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Trademark { get; set; }
        public string Introduction { get; set; }
    }
}