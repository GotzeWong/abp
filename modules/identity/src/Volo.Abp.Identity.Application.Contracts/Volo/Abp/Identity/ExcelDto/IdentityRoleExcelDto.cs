
using Npoi.Mapper.Attributes;

namespace Volo.Abp.Identity
{
    public class IdentityRoleExcelDto
    {
        public IdentityRoleExcelDto(IdentityRoleDto identityRoleDto)
        {
            Name = identityRoleDto.Name;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Column("角色")]
        public string Name { get; set; }
    }
}