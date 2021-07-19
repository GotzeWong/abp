using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DsPermissionManagement.Migrations
{
    public partial class UpdateDsPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Providers",
                table: "DsPermissionManagementDsPermissions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "DsPermissionManagementDsPermissions",
                type: "char(36)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Providers",
                table: "DsPermissionManagementDsPermissions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "DsPermissionManagementDsPermissions");
        }
    }
}
