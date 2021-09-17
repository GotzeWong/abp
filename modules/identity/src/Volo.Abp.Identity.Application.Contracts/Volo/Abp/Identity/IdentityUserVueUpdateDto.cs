using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace Volo.Abp.Identity
{
    public class IdentityUserVueUpdateDto : IdentityUserCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        [DisableAuditing]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        public string Password { get; set; }
        
        public string ConcurrencyStamp { get; set; }
        [CanBeNull]
        public Guid[] OrganizationUnitIds { get; set; }
        [CanBeNull]
        public Guid[] ChargeofOrgIds { get; set; }
    }
}