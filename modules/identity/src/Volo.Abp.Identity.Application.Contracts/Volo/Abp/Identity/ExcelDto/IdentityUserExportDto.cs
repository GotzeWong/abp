using Npoi.Mapper.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Volo.Abp.Identity
{
    public class IdentityUserExportDto
    {
        public IdentityUserExportDto(){}
        public IdentityUserExportDto(IdentityUserDto user)
        {
            this.UserName = user.UserName;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Column("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 花名
        /// </summary>
        [Column("花名")]
        public string Surname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("邮箱")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column("手机号")]
        [RegularExpression(@"^1\d{10}$", ErrorMessage = "非法手机号")]
        public string PhoneNumber { get; set; }


        [Column("角色")]
        public string RoleNames { get; set; }

        [Column("部门")]
        public string OrganizationUnitNames { get; set; }
    }
}
