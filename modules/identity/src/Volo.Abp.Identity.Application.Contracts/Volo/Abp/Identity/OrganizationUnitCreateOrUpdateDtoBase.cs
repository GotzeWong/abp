using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitCreateOrUpdateDtoBase : ExtensibleObject
    {
        [Required]
        [DynamicStringLength(typeof(OrganizationUnitConsts), nameof(OrganizationUnitConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }
        public Guid? ParentId { get; set; }
        public OrganizationUnitStatus Status { get; set; }
        public int OrderNo { get; set; }
        public string Remark { get; set; }
        public bool? isSale { get; set; }

        protected OrganizationUnitCreateOrUpdateDtoBase() : base(false)
        {
            
        }
    }
}