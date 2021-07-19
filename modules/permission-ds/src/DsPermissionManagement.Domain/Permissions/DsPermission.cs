using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace DsPermissionManagement.Permissions
{
    public class DsPermission : AuditedAggregateRoot<Guid>
    {
        public Guid? ParentId { get; private set; }

        public string Name { get; private set; }

        public string Discription { get; private set; }

        public DsPermissionStatus Status { get; private set; }

        public Guid? TenantId { get; private set; }

        public string Providers { get; private set; }

        public DsPermission()
        {

        }

        internal DsPermission(Guid id, Guid? parentId, string name, string discription, DsPermissionStatus status,Guid? tenantId,string providers) : base(id)
        {
            SetParentId(parentId);
            SetName(name);
            SetDiscription(discription);
            SetStatus(status);
            SetTenantId(tenantId);
            SetProviders(providers);
        }

        public DsPermission SetParentId(Guid? parentId)
        {
            ParentId = parentId;
            return this;
        }

        public DsPermission SetName([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            return this;
        }

        public DsPermission SetDiscription(string discription)
        {
            Discription = discription;
            return this;
        }

        public DsPermission SetStatus(DsPermissionStatus status)
        {
            Status = status;
            return this;
        }

        public DsPermission SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
            return this;
        }

        public DsPermission SetProviders(string providers)
        {
            Providers = providers;
            return this;
        }
    }
}
