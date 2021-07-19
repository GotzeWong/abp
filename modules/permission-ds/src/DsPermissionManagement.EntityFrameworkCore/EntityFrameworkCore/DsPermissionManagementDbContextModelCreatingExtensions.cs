using System;
using DsPermissionManagement.Permissions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DsPermissionManagement.EntityFrameworkCore
{
    public static class DsPermissionManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureDsPermissionManagement(
            this ModelBuilder builder,
            Action<DsPermissionManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DsPermissionManagementModelBuilderConfigurationOptions(
                DsPermissionManagementDbProperties.DbTablePrefix,
                DsPermissionManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<DsPermission>(b =>
            {
                b.ToTable(options.TablePrefix + "DsPermissions", options.Schema);
                b.ConfigureByConvention();
            });
        }
    }
}