
using Npoi.Mapper.Attributes;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitExcelDto
    {
        public OrganizationUnitExcelDto(){ }
        public OrganizationUnitExcelDto(OrganizationUnitDto organizationUnitDto)
        {
            Code = organizationUnitDto.Code;
            DisplayName = organizationUnitDto.DisplayName;
            Status = organizationUnitDto.Status;
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
        public OrganizationUnitStatus Status { get; set; }
        /// <summary>
        /// 是否销售部
        /// </summary>
        [Column("是否销售部")]
        public bool IsSale { get; set; }
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
    }
}
