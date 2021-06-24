using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Volo.Abp.TenantManagement
{
    public abstract class TenantCreateOrUpdateDtoBase : ExtensibleObject
    {
        [Required]
        [DynamicStringLength(typeof(TenantConsts), nameof(TenantConsts.MaxNameLength))]
        [Display(Name = "TenantName")]
        public string Name { get; set; }

        public string Trademark { get; set; }
        public string Introduction { get; set; }

        public TenantCreateOrUpdateDtoBase() : base(false)
        {

        }
    }
}