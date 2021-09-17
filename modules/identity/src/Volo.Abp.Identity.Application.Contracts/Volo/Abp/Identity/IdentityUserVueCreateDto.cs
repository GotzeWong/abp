using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Validation;

namespace Volo.Abp.Identity
{
    public class IdentityUserVueCreateDto : IdentityUserCreateOrUpdateDtoBase
    {
        [DisableAuditing]
        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        public string Password { get; set; }

        [CanBeNull]
        public Guid[] OrganizationUnitIds { get; set; }

        [CanBeNull]
        public Guid[] ChargeofOrgIds { get; set; }
    }
}