using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DsPermissionManagement.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
