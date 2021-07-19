using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsPermissionManagement.DsPermissions
{
    [Route("api/dsPermissionManager/permission")]
    public class DsPermissionController : DsPermissionManagementController, IDsPermissionAppService
    {
        private readonly IDsPermissionAppService _dsPermissionAppService;

        public DsPermissionController(IDsPermissionAppService dsPermissionAppService)
        {
            _dsPermissionAppService = dsPermissionAppService;
        }

        [HttpPost]
        public async Task<DsPermissionDto> CreateAsync(CreateDsPermissionDto input)
        {
            return await _dsPermissionAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _dsPermissionAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("tenantPermissionList")]
        public async Task<List<DsPermissionDto>> GetListAsync(Guid tenantId)
        {
            return await _dsPermissionAppService.GetListAsync(tenantId);
        }

        [HttpGet]
        public async Task<List<DsPermissionDto>> GetListAsync()
        {
            return await _dsPermissionAppService.GetListAsync();
        }

        [HttpPost]
        [Route("update")]
        public async Task<DsPermissionDto> UpdateAsync(Guid id, UpdateDsPermissionDto input)
        {
            return await _dsPermissionAppService.UpdateAsync(id, input);
        }
    }
}
