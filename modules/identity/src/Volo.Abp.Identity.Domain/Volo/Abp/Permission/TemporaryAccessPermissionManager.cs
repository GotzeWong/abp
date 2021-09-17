using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Threading;
using Volo.Abp.Uow;
using Volo.Abp.Settings;
using Volo.Abp.Identity.Settings;

namespace Volo.Abp.Identity
{
    public class TemporaryAccessPermissionManager : DomainService
    {
        protected ITemporaryAccessPermissionRepository TemporaryAccessPermissionRepository { get; }
        protected IIdentityUserRepository IdentityUserRepository { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        public TemporaryAccessPermissionManager(
            IIdentityUserRepository identityUserRepository,
            ITemporaryAccessPermissionRepository temporaryAccessPermissionRepository)
        {
            IdentityUserRepository = identityUserRepository;
            TemporaryAccessPermissionRepository = temporaryAccessPermissionRepository;
        }

        public async Task<TemporaryAccessPermission> CreateAsync(Guid GrantUserId, Guid TemporaryUserId, DateTime StartTime, DateTime EndTime, bool IsOpen, Guid? tenantId = null)
        {
            var GrantUser = await IdentityUserRepository.GetAsync(GrantUserId);
            var TemporaryUser = await IdentityUserRepository.GetAsync(TemporaryUserId);
            return new TemporaryAccessPermission(GuidGenerator.Create(), GrantUser, TemporaryUser, StartTime, EndTime, IsOpen, tenantId);
        }
    }
}
