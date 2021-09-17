using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class TemporaryAccessPermission : CreationAuditedEntity, IMultiTenant
    {
        public virtual Guid Id { get; protected set; }
        public virtual Guid? TenantId { get; protected set; }
        [ForeignKey("GrantUserId")]
        public virtual IdentityUser GrantUser { get; protected set; }
        [ForeignKey("TemporaryUserId")]
        public virtual IdentityUser TemporaryUser { get; protected set; }
        public virtual DateTime StartTime { get; protected set; }
        public virtual DateTime EndTime { get; protected set; }
        public virtual bool IsOpen { get; protected set; }

        public TemporaryAccessPermission() { }

        internal TemporaryAccessPermission([NotNull] Guid id, [NotNull] IdentityUser grantUser, [NotNull] IdentityUser temporaryUser, DateTime startTime, DateTime endTime, bool isOpen, Guid? tenantId = null)
        {
            SetId(id);
            SetGrantUser(grantUser);
            SetTemporary(temporaryUser);
            SetStartTime(startTime);
            SetEndTime(endTime);
            OpenOrClose(isOpen);
            TenantId = tenantId;
        }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public TemporaryAccessPermission SetId([NotNull] Guid id)
        {
            Check.NotNull(id, nameof(id));
            Id = id;
            return this;
        }

        public TemporaryAccessPermission SetGrantUser([NotNull] IdentityUser grantUser)
        {
            Check.NotNull(grantUser, nameof(grantUser));
            GrantUser = grantUser;
            return this;
        }

        public TemporaryAccessPermission SetTemporary([NotNull] IdentityUser temporaryUser)
        {
            Check.NotNull(temporaryUser, nameof(temporaryUser));
            TemporaryUser = temporaryUser;
            return this;
        }
        public TemporaryAccessPermission SetStartTime([NotNull] DateTime startTime)
        {
            Check.NotNull(startTime, nameof(startTime));
            StartTime = startTime;
            return this;
        }
        public TemporaryAccessPermission SetEndTime([NotNull] DateTime endTime)
        {
            Check.NotNull(endTime, nameof(endTime));
            EndTime = endTime;
            return this;
        }

        public TemporaryAccessPermission OpenOrClose([NotNull] bool isOpen)
        {
            IsOpen = isOpen;
            return this;
        }
    }
}
