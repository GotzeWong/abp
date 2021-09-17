
using Npoi.Mapper.Attributes;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitExportDto
    {
        public OrganizationUnitExportDto(){ }
        public OrganizationUnitExportDto(OrganizationUnitDto organizationUnitDto)
        {
            Code = organizationUnitDto.Code;
            DisplayName = organizationUnitDto.DisplayName;
            Status = organizationUnitDto.Status.ToString();
            Remark = organizationUnitDto.Remark;
            ParentName = OrganizationUnit.GetParentCode(organizationUnitDto.Code);
        }

        /// <summary>
        /// 代号
        /// </summary>
        [Column("代号")]
        public string Code { get; internal set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("部门名称")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        [Column("是否开启")]
        public string Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 上级部门
        /// </summary>
        [Column("上级部门")]
        public string ParentName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [Column("负责人")]
        public string LeaderNames { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        [Column("员工")]
        public string UserNames { get; set; }
    }
}
