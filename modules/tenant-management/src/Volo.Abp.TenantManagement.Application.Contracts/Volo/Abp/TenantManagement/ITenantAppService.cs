using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Volo.Abp.TenantManagement
{
    public interface ITenantAppService : ICrudAppService<TenantDto, Guid, GetTenantsInput, TenantCreateDto, TenantUpdateDto>
    {
        Task<string> GetDefaultConnectionStringAsync(Guid id);

        Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString);

        Task DeleteDefaultConnectionStringAsync(Guid id);

        Task UpdateTrademarkAsync(Guid id, string TrademarkUrl);
        Task<string> GetTrademarkAsync(Guid id);

    }
}
