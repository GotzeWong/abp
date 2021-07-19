using System.Threading.Tasks;
using System.Security.Principal;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Volo.Abp.Users;

namespace Volo.Abp.IdentityServer.AspNetIdentity
{
    public class AbpProfileService : ProfileService<IdentityUser>
    {
        protected ICurrentTenant CurrentTenant { get; }
        protected IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory { get; }

        protected IdentityUserManager _userManager;

        public AbpProfileService(
            IdentityUserManager userManager,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            ICurrentTenant currentTenant)
            : base(userManager, claimsFactory)
        {
            CurrentTenant = currentTenant;
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        [UnitOfWork]
        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            using (CurrentTenant.Change(context.Subject.FindTenantId()))
            {
                var userId = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub").Value;
                var user = await _userManager.FindByIdAsync(userId);
                var ClaimsPrincipal = await _claimsFactory.CreateAsync(user);
                context.Subject = ClaimsPrincipal;

                context.IssuedClaims = ClaimsPrincipal.Claims.ToList();

                await base.GetProfileDataAsync(context);
            }
        }

        [UnitOfWork]
        public override async Task IsActiveAsync(IsActiveContext context)
        {
            using (CurrentTenant.Change(context.Subject.FindTenantId()))
            {
                await base.IsActiveAsync(context);
            }
        }
    }
}
