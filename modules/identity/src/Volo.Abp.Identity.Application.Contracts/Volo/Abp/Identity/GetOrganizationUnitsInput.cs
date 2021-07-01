using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity
{
    public class GetOrganizationUnitsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
